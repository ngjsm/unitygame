using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipMotion : MonoBehaviour
{
    // 립 제스처 발생 시 실행할 이벤트 정의
    public delegate void LipAction();
    public static event LipAction OnLipOpen;

    void Update()
    {
        // 실제 구현: 립 제스처 인식 API의 결과로 대체할 것
        if (DetectLipOpenGesture())
        {
            OnLipOpen?.Invoke();
        }
    }

    // 테스트용: Space 키를 립 제스처로 가정
    bool DetectLipOpenGesture()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
