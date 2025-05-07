using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text instructionText;
    public TMP_Text resultText;

    [Header("���� ����")]
    public FruitType[] availableFruits = { FruitType.apple, FruitType.banana, FruitType.orange };
    public int minCount = 1;
    public int maxCount = 5;

    private FruitType targetFruit;
    private int targetCount;

    private int totalMoved = 0;
    private int correctMoved = 0;
    private bool isGameOver = false;

    void Start()
    {
        // 1) ���� ��ǥ ����
        targetFruit = availableFruits[Random.Range(0, availableFruits.Length)];
        targetCount = Random.Range(minCount, maxCount + 1);

        // 2) UI�� ���ù� ǥ��
        instructionText.text = $"{targetFruit} ������ {targetCount}��\n���ڿ� �Ű��ּ���!";
        resultText.text = "";
    }

    // BoxTrigger���� ȣ���
    public void OnFruitInBox(FruitType movedType)
    {
        if (isGameOver) return;

        totalMoved++;
        if (movedType == targetFruit) correctMoved++;

        // ��ǥ ������ŭ ������ �Ű����ٸ� ����
        if (totalMoved >= targetCount)
        {
            isGameOver = true;
            bool success = (correctMoved == targetCount);
            resultText.text = success
                ? "����! ��Ȯ�� �Ű���ϴ� ??"
                : $"���С� {targetFruit}�� {targetCount}�� �Űܾ� �߾��.";
        }
    }
}