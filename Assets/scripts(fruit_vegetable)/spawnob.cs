using TMPro;
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

    [Header("���� ���� UI")]
    [Tooltip("���� ���� �ؽ�Ʈ�� ���� ������Ʈ")]
    [SerializeField] private GameObject endTextObject;
    [Tooltip("���� ���� ����� ǥ���� TextMeshPro")]
    [SerializeField] private TextMeshProUGUI endText;

    private Transform spawnPoint;
    private GameObject currentObject;

    private int placedCount = 0;
    private const int maxPlaceCount = 10;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        spawnPoint = transform;
    }

    public void SpawnRandom()
    {


        if ((fruitPrefabs == null || fruitPrefabs.Length == 0) ||
            (trashPrefabs == null || trashPrefabs.Length == 0))
        {
            Debug.LogWarning("[spawnob] �������� ����ֽ��ϴ�.");
            return;
        }

        bool isFruit = Random.value < fruitSpawnProbability;
        var pool = isFruit ? fruitPrefabs : trashPrefabs;
        var prefab = pool[Random.Range(0, pool.Length)];

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
        placedCount++;

        if (placedCount >= maxPlaceCount)
        {
            EndGame();
        }
        else
        {
            SpawnRandom();
        }
    }

    private void EndGame()
    {
        gameEnded = true;

        if (endTextObject != null)
            endTextObject.SetActive(true);

        if (endText != null)
        {
            int score = gamemanager2.Instance.GetScore(); // ���� ��������
            endText.text = $"���� ����!\n����: {score} / {maxPlaceCount}";
        }

        Debug.Log("���� �����");
    }

    public bool IsGameEnded()
    {
        return gameEnded;
    }
}