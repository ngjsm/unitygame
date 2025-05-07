using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitmove : MonoBehaviour
{
    public float moveSpeed = 2f;                        // 이동 속도
    public Vector3 moveDirection = Vector3.forward;     // 앞으로 이동하는 방향 (월드 좌표 기준 z축)
    public float lifetime = 4f;                         // 과일 오브젝트가 존재하는 시간 (초)

    void Start()
    {
        // 과일이 생성된 후 lifetime 초 후에 자동 소멸
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 매 프레임마다 지정한 방향(Vector3.forward)으로 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}