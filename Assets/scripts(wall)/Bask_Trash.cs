using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    [Header("Bin Settings")]
    [Tooltip("과일 바구니인지 여부 (false면 쓰레기통)")]
    [SerializeField] private bool isFruitBin = true;

    private void OnTriggerEnter(Collider other)
    {
        // Rigidbody 없으면 무시
        if (!other.attachedRigidbody) return;

        // 올바른 바구니/통에 넣었는지 확인
        bool correct = (isFruitBin && other.CompareTag("Fruit"))
                    || (!isFruitBin && other.CompareTag("Trash"));

        if (correct) gamemanager2.Instance.AddScore(1);
        else gamemanager2.Instance.AddPenalty(1);

        // 오브젝트 삭제
        Destroy(other.gameObject);

        // 다음 오브젝트 스폰 요청
        spawnob.Instance.OnObjectPlaced();
    }
}