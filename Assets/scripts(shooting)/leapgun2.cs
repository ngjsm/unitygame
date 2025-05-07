using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;

public class leapgun2 : MonoBehaviour
{
    [Header("총/총구 설정")]
    public Transform gunTransform;
    public Transform firePoint;

    [Header("총알 프리팹")]
    public GameObject projectilePrefab;

    [Header("회전 및 발사 제어")]
    public float rotationSmoothSpeed = 5f;
    public float shootCooldown = 0.5f;
    public float pinchThreshold = 0.8f;

    private float lastShotTime;

    [Header("테스트/시뮬레이션용 설정")]
    public bool simulationMode = false;
    public GameObject rightHandObject;
    public float simulatedPinchStrength = 0f;

    // XR Hand 관련 변수
    private XRHandSubsystem handSubsystem;

    void Start()
    {
        var subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetInstances(subsystems);
        if (subsystems.Count > 0)
        {
            handSubsystem = subsystems[0];
        }
    }

    void Update()
    {
        if (simulationMode || rightHandObject != null)
        {
            Quaternion handRotation;
            float pinchStrength;

            if (simulationMode)
            {
                float rotationInput = Input.GetAxis("Horizontal");
                handRotation = Quaternion.Euler(0f, rotationInput * 45f, 0f);
                pinchStrength = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            }
            else
            {
                handRotation = rightHandObject.transform.rotation;

                // XR Hand 사용: 실제 핀치 강도 계산
                pinchStrength = GetRightHandPinchStrength();
            }

            gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation, handRotation, rotationSmoothSpeed * Time.deltaTime);

            if (pinchStrength >= pinchThreshold && (Time.time - lastShotTime) > shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
        else
        {
            Debug.LogWarning("오른손 데이터를 가져올 수 없습니다. simulationMode를 켜거나, rightHandObject를 Inspector에서 할당하세요.");
        }
    }

    float GetRightHandPinchStrength()
    {
        if (handSubsystem == null)
            return simulatedPinchStrength;

        var rightHand = handSubsystem.rightHand;
        if (!rightHand.isTracked)
            return simulatedPinchStrength;

        var thumbTip = rightHand.GetJoint(XRHandJointID.ThumbTip);
        var indexTip = rightHand.GetJoint(XRHandJointID.IndexTip);

        if (!thumbTip.TryGetPose(out Pose thumbPose) || !indexTip.TryGetPose(out Pose indexPose))
            return simulatedPinchStrength;

        float distance = Vector3.Distance(thumbPose.position, indexPose.position);
        float normalized = Mathf.Clamp01(1f - (distance / 0.05f)); // 0.05m 이하 = 완전 핀치
        return normalized;
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}