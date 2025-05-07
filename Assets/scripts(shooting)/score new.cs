using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scorenew : MonoBehaviour
{
    public static scorenew Instance;

    public int targetHit = 0;
    public int otherHit = 0;
    public TMP_Text targetText;
    public TMP_Text scoreText;
    public TMP_Text badScoreText;

    private string targetName = "";

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void SetTargetName(string name)
    {
        targetName = name;
        UpdateUI();
    }

    public void AddTargetHit()
    {
        targetHit++;
        UpdateUI();
    }

    public void AddOtherHit()
    {
        otherHit++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (targetText != null)
            targetText.text = $"Target: {targetName}";
        if (scoreText != null)
            scoreText.text = $"Score: {targetHit}";
        if (badScoreText != null)
            badScoreText.text = $"Bad Score: {otherHit}";
    }
}
