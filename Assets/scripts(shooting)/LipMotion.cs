using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipMotion : MonoBehaviour
{
    // �� ����ó �߻� �� ������ �̺�Ʈ ����
    public delegate void LipAction();
    public static event LipAction OnLipOpen;

    void Update()
    {
        // ���� ����: �� ����ó �ν� API�� ����� ��ü�� ��
        if (DetectLipOpenGesture())
        {
            OnLipOpen?.Invoke();
        }
    }

    // �׽�Ʈ��: Space Ű�� �� ����ó�� ����
    bool DetectLipOpenGesture()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
