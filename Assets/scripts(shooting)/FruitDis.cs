using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDis : MonoBehaviour
{
    // ������ ����ִ� �ð� (�� ����)
    public float lifetime = 5f;

    void Start()
    {
        // ������ lifetime �Ŀ� ���� ������Ʈ(gameObject)�� �ı���
        Destroy(gameObject, lifetime);
    }
}
