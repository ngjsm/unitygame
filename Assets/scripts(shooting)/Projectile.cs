using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [Header("총알 속성")]
    [SerializeField] private float speed = 20f;      // 총알 이동 속도
    [SerializeField] private float lifetime = 2f;    // 총알이 존재하는 시간 (초)

    void Start()
    {
        // lifetime 이후 총알을 자동 삭제하여 메모리 누수 방지
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 총알이 자신의 forward 방향으로 이동하도록 함
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 먼저 Target 태그를 체크
        if (other.CompareTag("Target"))
        {
            if (score.Instance != null)
                score.Instance.AddTargetHit();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // 그 다음 Fruit 태그인 경우 처리
        else if (other.CompareTag("Fruit"))
        {
            if (score.Instance != null)
                score.Instance.AddOtherHit();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // 플레이어가 아닌 다른 오브젝트와 충돌 시 총알만 제거
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}