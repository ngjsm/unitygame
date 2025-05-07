using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager Instance;

    public bool gameStarted = false;
    public GameObject startButton; // Start 버튼 오브젝트 연결 (큐브)

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        gameStarted = true;
        if (startButton != null)
            startButton.SetActive(false);  // 버튼 숨기기
        Debug.Log("게임 시작됨!");
    }

    public void ResetGame()
    {
        gameStarted = false;
        if (startButton != null)
            startButton.SetActive(true);  // 버튼 다시 보여줌
        Debug.Log("게임 리셋됨!");
    }
}
