using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text instructionText;
    public TMP_Text resultText;

    [Header("게임 설정")]
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
        // 1) 랜덤 목표 설정
        targetFruit = availableFruits[Random.Range(0, availableFruits.Length)];
        targetCount = Random.Range(minCount, maxCount + 1);

        // 2) UI에 지시문 표시
        instructionText.text = $"{targetFruit} 과일을 {targetCount}개\n상자에 옮겨주세요!";
        resultText.text = "";
    }

    // BoxTrigger에서 호출됨
    public void OnFruitInBox(FruitType movedType)
    {
        if (isGameOver) return;

        totalMoved++;
        if (movedType == targetFruit) correctMoved++;

        // 목표 개수만큼 과일이 옮겨졌다면 종료
        if (totalMoved >= targetCount)
        {
            isGameOver = true;
            bool success = (correctMoved == targetCount);
            resultText.text = success
                ? "성공! 정확히 옮겼습니다 ??"
                : $"실패… {targetFruit}만 {targetCount}개 옮겨야 했어요.";
        }
    }
}