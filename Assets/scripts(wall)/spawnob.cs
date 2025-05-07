using UnityEngine;

public class spawnob : MonoBehaviour
{
    public static spawnob Instance { get; private set; }

    [Header("Test Controls")]
    [SerializeField] private bool enableKeyboardControl = false;

    [Header("Spawn Settings")]
    [Tooltip("������ ���� ������ ���")]
    [SerializeField] private GameObject[] fruitPrefabs;
    [Tooltip("������ ������ ������ ���")]
    [SerializeField] private GameObject[] trashPrefabs;

    [Header("Probability")]
    [Range(0f, 1f), Tooltip("���� ���� Ȯ��")]
    [SerializeField] private float fruitSpawnProbability = 0.7f;

    [Header("Spawn Area")]
    [Tooltip("���� �ݰ� (m)")]
    [SerializeField] private float spawnRadius = 0.5f;

    private Transform spawnPoint;
    private GameObject currentObject;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        spawnPoint = transform;
    }

    private void Start()
    {
        SpawnRandom();
    }

    /// <summary>
    /// ���� ������Ʈ�� ���� ���� ���� �Ǵ� �����⸦ �����մϴ�.
    /// </summary>
    public void SpawnRandom()
    {
        if (currentObject != null) return;

        if ((fruitPrefabs == null || fruitPrefabs.Length == 0) ||
            (trashPrefabs == null || trashPrefabs.Length == 0))
        {
            Debug.LogWarning("[ObjectSpawner] Prefab �迭�� ����ֽ��ϴ�.");
            return;
        }

        // ����/������ ����
        bool isFruit = Random.value < fruitSpawnProbability;
        var pool = isFruit ? fruitPrefabs : trashPrefabs;
        var prefab = pool[Random.Range(0, pool.Length)];

        // ���� �ݰ� �� ���� ��ġ ���
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