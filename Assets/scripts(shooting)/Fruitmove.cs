using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitmove : MonoBehaviour
{
    public float moveSpeed = 2f;                        // �̵� �ӵ�
    public Vector3 moveDirection = Vector3.forward;     // ������ �̵��ϴ� ���� (���� ��ǥ ���� z��)
    public float lifetime = 4f;                         // ���� ������Ʈ�� �����ϴ� �ð� (��)

    void Start()
    {
        // ������ ������ �� lifetime �� �Ŀ� �ڵ� �Ҹ�
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �� �����Ӹ��� ������ ����(Vector3.forward)���� �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}