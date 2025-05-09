using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardtest : MonoBehaviour
{
    [Header("Ű���� �̵� �ӵ�")]
    [SerializeField] private float moveSpeed = 2f;

    void Update()
    {
        // ���򡤼��� �Է� (A/D or ���, W/S or ���)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ������ �̵� (Q��, E��)
        float y = 0f;
        if (Input.GetKey(KeyCode.Q)) y += 1f;
        else if (Input.GetKey(KeyCode.E)) y -= 1f;

        Vector3 dir = new Vector3(x, y, z) * moveSpeed * Time.deltaTime;
        transform.Translate(dir, Space.World);
    }
}
