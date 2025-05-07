using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tageting : MonoBehaviour
{
    [Header("UI 연결")]
    public TMP_Text targetText;

    [Header("타겟 체크 주기 (초)")]
    public float checkInterval = 1f;

    private float timer = 0f;

    void Start()
    {
        // 과일이 스폰될 시간을 고려하여 지연 후 타겟을 지정합니다.
        Invoke("DelayedPickNewTarget", 1.5f);
    }

    void DelayedPickNewTarget()
    {
        // "Fruit" 태그를 가진 과일이 있는지 확인
        if (GameObject.FindGameObjectWithTag("Target") == null)
        {
            PickNewTarget();
            UpdateTargetText();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            // 타겟 오브젝트가 없다면 새 타겟 지정 및 텍스트 업데이트
            if (GameObject.FindGameObjectWithTag("Target") == null)
            {
                PickNewTarget();
                UpdateTargetText();
            }
        }
    }

    void PickNewTarget()
    {
        // "Fruit" 태그를 가진 모든 과일 찾기
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        if (fruits.Length > 0)
        {
            int index = Random.Range(0, fruits.Length);
            GameObject newTarget = fruits[index];
            newTarget.tag = "Target";  // 타겟 지정 (태그만 변경)
            Debug.Log("새 타겟: " + newTarget.name);
        }
    }

    void UpdateTargetText()
    {
        if (targetText != null)
        {
            GameObject targetObj = GameObject.FindGameObjectWithTag("Target");
            if (targetObj != null)
            {
                targetText.text = "Target Fruit: " + targetObj.name;
                Debug.Log("업데이트된 텍스트: " + targetText.text);
            }
            else
            {
                targetText.text = "Target Fruit: 없음";
            }
        }
    }
}