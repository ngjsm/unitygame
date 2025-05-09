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
        if (!other.attachedRigidbody) return;

        // 게임 종료 시 무시
        if (spawnob.Instance.IsGameEnded()) return;

        // 성공/실패 판정
        bool correct = (isFruitBin && other.CompareTag("Fruit"))
                    || (!isFruitBin && other.CompareTag("Trash"));

        if (correct)
            gamemanager2.Instance.AddScore(1);
        else
            gamemanager2.Instance.AddPenalty(1);

        // ✅ 오브젝트 파괴 대신 숨김 처리
        other.gameObject.SetActive(false);

        // 다음 오브젝트 요청
        spawnob.Instance.OnObjectPlaced();
    }
}
