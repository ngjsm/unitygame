using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    [Header("Bin Settings")]
    [Tooltip("���� �ٱ������� ���� (false�� ��������)")]
    [SerializeField] private bool isFruitBin = true;

    private void OnTriggerEnter(Collider other)
    {
        // Rigidbody ������ ����
        if (!other.attachedRigidbody) return;

        // �ùٸ� �ٱ���/�뿡 �־����� Ȯ��
        bool correct = (isFruitBin && other.CompareTag("Fruit"))
                    || (!isFruitBin && other.CompareTag("Trash"));

        if (correct) gamemanager2.Instance.AddScore(1);
        else gamemanager2.Instance.AddPenalty(1);

        // ������Ʈ ����
        Destroy(other.gameObject);

        // ���� ������Ʈ ���� ��û
        spawnob.Instance.OnObjectPlaced();
    }
}