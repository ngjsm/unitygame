using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttontrig : MonoBehaviour
{
    public string sceneToLoad = "NextSceneName"; // �̵��ϰ� ���� �� �̸��� ��������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger"))
        {
            Debug.Log("��ư�� ���Ƚ��ϴ�!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}