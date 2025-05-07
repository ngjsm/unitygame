using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("과일 프리팹")]
    public GameObject applePrefab;
    public GameObject bananaPrefab;
    public GameObject orangePrefab;

    [Header("스폰 영역(테이블 위)")]
    public Vector3 areaCenter;
    public Vector3 areaSize;

    [Header("스폰 설정")]
    public int initialCountPerType = 5;
    public float spawnCheckRadius = 0.4f;      // 과일 반지름보다 살짝 크게
    public LayerMask fruitLayerMask;           // “과일” 레이어만 검사

    private void Start()
    {
        for (int i = 0; i < initialCountPerType; i++)
            SpawnRandomFruit();
    }

    public void SpawnRandomFruit()
    {
        int idx = Random.Range(0, 3);
        SpawnFruit((FruitType)idx);
    }

    public void SpawnFruit(FruitType type)
    {
        GameObject prefab = null;
        switch (type)
        {
            case FruitType.apple: prefab = applePrefab; break;
            case FruitType.banana: prefab = bananaPrefab; break;
            case FruitType.orange: prefab = orangePrefab; break;
        }
        if (prefab == null) return;

        Vector3 pos = GetNonOverlappingPosition();
        Instantiate(prefab, pos, Quaternion.identity);
    }

    private Vector3 GetNonOverlappingPosition()
    {
        const int maxAttempts = 20;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // 영역 내 랜덤 위치 생성
            float x = areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = areaCenter.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
            float y = areaCenter.y;
            Vector3 candidate = new Vector3(x, y, z);

            // 반지름 spawnCheckRadius 만큼 구 형태로 겹침 검사
            bool overlaps = Physics.CheckSphere(candidate, spawnCheckRadius, fruitLayerMask);
            if (!overlaps)
                return candidate;
        }

        // 만약 모두 실패했다면 그냥 마지막 후보 반환
        Debug.LogWarning("SpawnManager: 겹침 없는 위치 찾기 실패, 강제 스폰");
        return new Vector3(
            areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            areaCenter.y,
            areaCenter.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
        );
    }

    // (기존 GetRandomPosition 메서드는 사용하지 않습니다)
}