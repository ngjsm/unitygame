using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Tracking.OpenXR;
using UnityEngine;


public class leapguncontroller : MonoBehaviour
{
    [Header("총/총구 설정")]
    [Tooltip("총 또는 총구 역할을 할 Transform (회전이 적용될 대상)")]
    public Transform gunTransform;

    [Tooltip("총알이 생성될 위치를 나타내는 총구(빈 GameObject)")]
    public Transform firePoint;

    [Header("총알 프리팹")]
    [Tooltip("Instantiate할 총알 프리팹 (예: Cube에 Projectile 스크립트가 붙은 프리팹)")]
    public GameObject projectilePrefab;

    [Header("회전 및 발사 제어")]
    [Tooltip("회전의 부드러운 전환 속도")]
    public float rotationSmoothSpeed = 5f;
    [Tooltip("연속 발사를 막기 위한 쿨다운 시간 (초)")]
    public float shootCooldown = 0.5f;
    [Tooltip("발사 조건으로 사용할 핀치 임계값 (0 ~ 1 사이)")]
    public float pinchThreshold = 0.8f;

    private float lastShotTime;

    [Header("테스트/시뮬레이션용 설정")]
    [Tooltip("시뮬레이션 모드를 사용하면, 키보드 입력으로 손 회전 및 발사를 테스트합니다.")]
    public bool simulationMode = false;

    [Tooltip("실제 씬에 있는 오른손 오브젝트를 사용하고 싶다면, 여기에 할당하세요.")]
    public GameObject rightHandObject;

    [Tooltip("만약 실제 핀치 강도 데이터를 가져올 수 없다면, 시뮬레이션 시 사용될 값 (0~1)")]
    public float simulatedPinchStrength = 0f;

    void Update()
    {
        // simulationMode가 활성화 되어있거나, 오른손 오브젝트를 할당한 경우
        if (simulationMode || rightHandObject != null)
        {
            Quaternion handRotation;
            float pinchStrength;

            if (simulationMode)
            {
                // 시뮬레이션: 좌우 화살표 키를 사용하여 회전(예, -45도 ~ 45도)
                float rotationInput = Input.GetAxis("Horizontal");
                handRotation = Quaternion.Euler(0f, rotationInput * 45f, 0f);

                // 스페이스바를 누르면 핀치 강도 1, 아니면 0
                pinchStrength = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            }
            else
            {
                // 실제 손 오브젝트에서 회전 값을 가져옵니다.
                handRotation = rightHandObject.transform.rotation;
                // 실제로 핀치 강도 데이터를 제공하는 컴포넌트가 있다면 여기서 가져오세요.
                // 없다면 Inspector에서 simulatedPinchStrength 값을 조정하여 테스트할 수 있습니다.
                pinchStrength = simulatedPinchStrength;
            }

            // 총의 회전을 부드럽게 보간하여 적용합니다.
            gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation, handRotation, rotationSmoothSpeed * Time.deltaTime);

            // 발사 조건: 핀치 강도가 임계값 이상이고 쿨다운 시간이 지난 경우
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

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            // 필요하다면 사운드나 파티클 이펙트도 추가할 수 있습니다.
        }
    }
}