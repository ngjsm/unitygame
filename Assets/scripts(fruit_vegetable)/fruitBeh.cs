using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitBeh : MonoBehaviour
{
    public void PrepareForDestroy()
    {
        Joint[] joints = GetComponents<Joint>();
        foreach (var joint in joints)
        {
            Destroy(joint);
        }
    }
}
