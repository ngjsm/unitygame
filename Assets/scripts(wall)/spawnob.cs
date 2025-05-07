using UnityEngine;

public class spawnob : MonoBehaviour
{
    public static spawnob Instance { get; private set; }

    [Header("Test Controls")]
    [SerializeField] private bool enableKeyboardControl = false;

    [Header("Spawn Settings")]
    [Tooltip("생성할 과일 프리팹 목록")]
    [SerializeField] private GameObject[] fruitPrefabs;
    [Tooltip("생성할 쓰레기 프리팹 목록")]
    [SerializeField] private GameObject[] trashPrefabs;

    [Header("Probability")]
    [Range(0f, 1f), Tooltip("과일 스폰 확률")]
    [SerializeField] private float fruitSpawnProbability = 0.7f;

    [Header("Spawn Area")]
    [Tooltip("스폰 반경 (m)")]
    [SerializeField] private float spawnRadius = 0.5f;

    private Transform spawnPoint;
    private GameObject currentObject;

    private void Awake()
    {
        // 싱글턴 설정
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        spawnPoint = transform;
    }

    private void Start()
    {
        SpawnRandom();
    }

    /// <summary>
    /// 현재 오브젝트가 없을 때만 과일 또는 쓰레기를 스폰합니다.
    /// </summary>
    public void SpawnRandom()
    {
        if (currentObject != null) return;

        if ((fruitPrefabs == null || fruitPrefabs.Length == 0) ||
            (trashPrefabs == null || trashPrefabs.Length == 0))
        {
            Debug.LogWarning("[ObjectSpawner] Prefab 배열이 비어있습니다.");
            return;
        }

        // 과일/쓰레기 선택
        bool isFruit = Random.value < fruitSpawnProbability;
        var pool = isFruit ? fruitPrefabs : trashPrefabs;
        var prefab = pool[Random.Range(0, pool.Length)];

        // 원형 반경 내 랜덤 위치 계산
        var offset = Random.insideUnitCircle * spawnRadius;
        var pos = spawnPoint.position + new Vector3(offset.x, 0f, offset.y);

        currentObject = Instantiate(prefab, pos, Quaternion.identity);

        if (enableKeyboardControl)
        {
            currentObject.AddComponent<keyboardtest>();
        }
    }


    public void OnObjectPlaced()
    {
        currentObject = null;
        SpawnRandom();
    }
}