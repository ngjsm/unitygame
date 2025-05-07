using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("���� ������")]
    public GameObject applePrefab;
    public GameObject bananaPrefab;
    public GameObject orangePrefab;

    [Header("���� ����(���̺� ��)")]
    public Vector3 areaCenter;
    public Vector3 areaSize;

    [Header("���� ����")]
    public int initialCountPerType = 5;
    public float spawnCheckRadius = 0.4f;      // ���� ���������� ��¦ ũ��
    public LayerMask fruitLayerMask;           // �����ϡ� ���̾ �˻�

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
            // ���� �� ���� ��ġ ����
            float x = areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = areaCenter.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
            float y = areaCenter.y;
            Vector3 candidate = new Vector3(x, y, z);

            // ������ spawnCheckRadius ��ŭ �� ���·� ��ħ �˻�
            bool overlaps = Physics.CheckSphere(candidate, spawnCheckRadius, fruitLayerMask);
            if (!overlaps)
                return candidate;
        }

        // ���� ��� �����ߴٸ� �׳� ������ �ĺ� ��ȯ
        Debug.LogWarning("SpawnManager: ��ħ ���� ��ġ ã�� ����, ���� ����");
        return new Vector3(
            areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            areaCenter.y,
            areaCenter.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
        );
    }

    // (���� GetRandomPosition �޼���� ������� �ʽ��ϴ�)
}