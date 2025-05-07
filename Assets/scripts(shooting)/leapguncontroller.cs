using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Tracking.OpenXR;
using UnityEngine;


public class leapguncontroller : MonoBehaviour
{
    [Header("��/�ѱ� ����")]
    [Tooltip("�� �Ǵ� �ѱ� ������ �� Transform (ȸ���� ����� ���)")]
    public Transform gunTransform;

    [Tooltip("�Ѿ��� ������ ��ġ�� ��Ÿ���� �ѱ�(�� GameObject)")]
    public Transform firePoint;

    [Header("�Ѿ� ������")]
    [Tooltip("Instantiate�� �Ѿ� ������ (��: Cube�� Projectile ��ũ��Ʈ�� ���� ������)")]
    public GameObject projectilePrefab;

    [Header("ȸ�� �� �߻� ����")]
    [Tooltip("ȸ���� �ε巯�� ��ȯ �ӵ�")]
    public float rotationSmoothSpeed = 5f;
    [Tooltip("���� �߻縦 ���� ���� ��ٿ� �ð� (��)")]
    public float shootCooldown = 0.5f;
    [Tooltip("�߻� �������� ����� ��ġ �Ӱ谪 (0 ~ 1 ����)")]
    public float pinchThreshold = 0.8f;

    private float lastShotTime;

    [Header("�׽�Ʈ/�ùķ��̼ǿ� ����")]
    [Tooltip("�ùķ��̼� ��带 ����ϸ�, Ű���� �Է����� �� ȸ�� �� �߻縦 �׽�Ʈ�մϴ�.")]
    public bool simulationMode = false;

    [Tooltip("���� ���� �ִ� ������ ������Ʈ�� ����ϰ� �ʹٸ�, ���⿡ �Ҵ��ϼ���.")]
    public GameObject rightHandObject;

    [Tooltip("���� ���� ��ġ ���� �����͸� ������ �� ���ٸ�, �ùķ��̼� �� ���� �� (0~1)")]
    public float simulatedPinchStrength = 0f;

    void Update()
    {
        // simulationMode�� Ȱ��ȭ �Ǿ��ְų�, ������ ������Ʈ�� �Ҵ��� ���
        if (simulationMode || rightHandObject != null)
        {
            Quaternion handRotation;
            float pinchStrength;

            if (simulationMode)
            {
                // �ùķ��̼�: �¿� ȭ��ǥ Ű�� ����Ͽ� ȸ��(��, -45�� ~ 45��)
                float rotationInput = Input.GetAxis("Horizontal");
                handRotation = Quaternion.Euler(0f, rotationInput * 45f, 0f);

                // �����̽��ٸ� ������ ��ġ ���� 1, �ƴϸ� 0
                pinchStrength = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            }
            else
            {
                // ���� �� ������Ʈ���� ȸ�� ���� �����ɴϴ�.
                handRotation = rightHandObject.transform.rotation;
                // ������ ��ġ ���� �����͸� �����ϴ� ������Ʈ�� �ִٸ� ���⼭ ����������.
                // ���ٸ� Inspector���� simulatedPinchStrength ���� �����Ͽ� �׽�Ʈ�� �� �ֽ��ϴ�.
                pinchStrength = simulatedPinchStrength;
            }

            // ���� ȸ���� �ε巴�� �����Ͽ� �����մϴ�.
            gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation, handRotation, rotationSmoothSpeed * Time.deltaTime);

            // �߻� ����: ��ġ ������ �Ӱ谪 �̻��̰� ��ٿ� �ð��� ���� ���
            if (pinchStrength >= pinchThreshold && (Time.time - lastShotTime) > shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
        else
        {
            Debug.LogWarning("������ �����͸� ������ �� �����ϴ�. simulationMode�� �Ѱų�, rightHandObject�� Inspector���� �Ҵ��ϼ���.");
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            // �ʿ��ϴٸ� ���峪 ��ƼŬ ����Ʈ�� �߰��� �� �ֽ��ϴ�.
        }
    }
}