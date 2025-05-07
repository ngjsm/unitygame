using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;

public class gunPlugin : MonoBehaviour
{
    XRHandSubsystem handSubsystem;

    [Header("Gun Settings")]
    public Transform gunTransform;          // �Ѹ��� �پ� �ִ� Transform
    public Transform firePoint;            // �ѱ� ��ġ Empty
    public GameObject projectilePrefab;
    public float rotationSmooth = 5f;      // ȸ�� ������ �ӵ�
    public float shootCooldown = 0.5f;     // ���ӹ߻� ����
    [Tooltip("�� cm �̳��� ����-���� ���� ���� �߻�����(���� ����)")]
    public float pinchDistance = 0.02f;    // 2cm

    [Header("Translation-to-Yaw Settings")]
    [Tooltip("���� �������� 1m �̵����� �� �󸶳� ȸ������ (��)")]
    public float yawSensitivity = 100f;
    [Tooltip("�ִ� �ν��� �� �̵� �Ÿ� (m)")]
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
        else Debug.LogError("�� XRHandSubsystem�� ã�� ���߽��ϴ�!");
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
        // �� ���� �����Ӻ��ʹ� ���� ���� �̵����� ����� yaw�� ����
        else if (right.GetJoint(XRHandJointID.Palm).TryGetPose(out Pose palmPose))
        {
            // ���� x�� �̵��� ���ϱ� (Ŭ����)
            float dx = Mathf.Clamp(palmPose.position.x - initialPalmPos.x,
                                   -maxPalmMovement,
                                    maxPalmMovement);
            // �̵��� �� �ΰ��� �� ��ǥ yaw
            float targetYaw = initialYaw + dx * yawSensitivity;
            Quaternion targetRot = Quaternion.Euler(0f, targetYaw, 0f);

            // �ε巴�� ����
            gunTransform.rotation = Quaternion.Slerp(
                gunTransform.rotation,
                targetRot,
                rotationSmooth * Time.deltaTime
            );
        }

        // 3) �߻� ����: ���� ��(ThumbTip)�� ���� ��(IndexTip) �Ÿ� ��
        if (right.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumb) &&
             right.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose idx))
        {
            float dist = Vector3.Distance(thumb.position, idx.position);
            // ����׷� ��ġ �Ÿ��� �� ������ �� threshold ������ ������
            Debug.Log($"PinchDist = {dist:F3} m");

            if (dist <= pinchDistance && Time.time - lastShotTime > shootCooldown)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                lastShotTime = Time.time;
            }
        }
    }
}