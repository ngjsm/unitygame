using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;

public class gunPlugin : MonoBehaviour
{
    XRHandSubsystem handSubsystem;

    [Header("Gun Settings")]
    public Transform gunTransform;          // 총모델이 붙어 있는 Transform
    public Transform firePoint;            // 총구 위치 Empty
    public GameObject projectilePrefab;
    public float rotationSmooth = 5f;      // 회전 스무스 속도
    public float shootCooldown = 0.5f;     // 연속발사 간격
    [Tooltip("몇 cm 이내로 엄지-검지 끝이 오면 발사할지(미터 단위)")]
    public float pinchDistance = 0.02f;    // 2cm

    [Header("Translation-to-Yaw Settings")]
    [Tooltip("손이 수평으로 1m 이동했을 때 얼마나 회전할지 (°)")]
    public float yawSensitivity = 100f;
    [Tooltip("최대 인식할 손 이동 거리 (m)")]
    public float maxPalmMovement = 0.2f;

    bool calibrated = false;
    Vector3 initialPalmPos;
    float initialYaw;

    float lastShotTime;

    void Start()
    {
        var subs = new List<XRHandSubsystem>();
        SubsystemManager.GetInstances(subs);
        if (subs.Count > 0) handSubsystem = subs[0];
        else Debug.LogError("▶ XRHandSubsystem을 찾지 못했습니다!");
    }

    void Update()
    {
        if (handSubsystem == null) return;

        XRHand right = handSubsystem.rightHand;
        if (!right.isTracked) return;

 
        if (!calibrated)
        {
            if (right.GetJoint(XRHandJointID.Palm).TryGetPose(out Pose palmPose))
            {
                initialPalmPos = palmPose.position;
                initialYaw = gunTransform.rotation.eulerAngles.y;
                calibrated = true;
            }
        }
        // 그 다음 프레임부터는 손의 수평 이동량에 비례해 yaw만 변경
        else if (right.GetJoint(XRHandJointID.Palm).TryGetPose(out Pose palmPose))
        {
            // 손의 x축 이동량 구하기 (클램프)
            float dx = Mathf.Clamp(palmPose.position.x - initialPalmPos.x,
                                   -maxPalmMovement,
                                    maxPalmMovement);
            // 이동량 × 민감도 → 목표 yaw
            float targetYaw = initialYaw + dx * yawSensitivity;
            Quaternion targetRot = Quaternion.Euler(0f, targetYaw, 0f);

            // 부드럽게 보간
            gunTransform.rotation = Quaternion.Slerp(
                gunTransform.rotation,
                targetRot,
                rotationSmooth * Time.deltaTime
            );
        }

        // 3) 발사 조건: 엄지 끝(ThumbTip)과 검지 끝(IndexTip) 거리 비교
        if (right.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumb) &&
             right.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose idx))
        {
            float dist = Vector3.Distance(thumb.position, idx.position);
            // 디버그로 핀치 거리를 매 프레임 찍어서 threshold 조정해 보세요
            Debug.Log($"PinchDist = {dist:F3} m");

            if (dist <= pinchDistance && Time.time - lastShotTime > shootCooldown)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                lastShotTime = Time.time;
            }
        }
    }
}