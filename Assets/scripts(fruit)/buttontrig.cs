using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttontrig : MonoBehaviour
{
    public string sceneToLoad = "NextSceneName"; // 이동하고 싶은 씬 이름을 적으세요

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger"))
        {
            Debug.Log("버튼이 눌렸습니다!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}