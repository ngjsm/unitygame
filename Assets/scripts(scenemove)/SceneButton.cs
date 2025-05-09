using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public string sceneName;  // 이 버튼이 연결된 씬 이름

    public void Highlight(bool isSelected)
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = isSelected ? Color.yellow : Color.white;
        }
    }
}