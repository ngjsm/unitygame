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
        // 손에서 강제로 놓이도록 모든 joint 제거
        var rigid = GetComponent<Rigidbody>();
        if (rigid != null)
        {
            var joints = rigid.GetComponents<Joint>();
            foreach (var joint in joints)
            {
                Destroy(joint);
            }
        }

        yield return new WaitForSeconds(0.05f);  // 아주 짧은 지연

        Destroy(gameObject);
    }
}
