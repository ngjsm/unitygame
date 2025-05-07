using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tageting : MonoBehaviour
{
    [Header("UI ����")]
    public TMP_Text targetText;

    [Header("Ÿ�� üũ �ֱ� (��)")]
    public float checkInterval = 1f;

    private float timer = 0f;

    void Start()
    {
        // ������ ������ �ð��� ����Ͽ� ���� �� Ÿ���� �����մϴ�.
        Invoke("DelayedPickNewTarget", 1.5f);
    }

    void DelayedPickNewTarget()
    {
        // "Fruit" �±׸� ���� ������ �ִ��� Ȯ��
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
            // Ÿ�� ������Ʈ�� ���ٸ� �� Ÿ�� ���� �� �ؽ�Ʈ ������Ʈ
            if (GameObject.FindGameObjectWithTag("Target") == null)
            {
                PickNewTarget();
                UpdateTargetText();
            }
        }
    }

    void PickNewTarget()
    {
        // "Fruit" �±׸� ���� ��� ���� ã��
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        if (fruits.Length > 0)
        {
            int index = Random.Range(0, fruits.Length);
            GameObject newTarget = fruits[index];
            newTarget.tag = "Target";  // Ÿ�� ���� (�±׸� ����)
            Debug.Log("�� Ÿ��: " + newTarget.name);
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
                Debug.Log("������Ʈ�� �ؽ�Ʈ: " + targetText.text);
            }
            else
            {
                targetText.text = "Target Fruit: ����";
            }
        }
    }
}