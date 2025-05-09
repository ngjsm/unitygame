using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsup : MonoBehaviour
{
    public void ForceDestroy()
    {
        StartCoroutine(ReleaseAndDestroy());
    }

    private IEnumerator ReleaseAndDestroy()
    {
        // �տ��� ������ ���̵��� ��� joint ����
        var rigid = GetComponent<Rigidbody>();
        if (rigid != null)
        {
            var joints = rigid.GetComponents<Joint>();
            foreach (var joint in joints)
            {
                Destroy(joint);
            }
        }

        yield return new WaitForSeconds(0.05f);  // ���� ª�� ����

        Destroy(gameObject);
    }
}
