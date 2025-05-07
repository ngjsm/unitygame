using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDis : MonoBehaviour
{
    // 과일이 살아있는 시간 (초 단위)
    public float lifetime = 5f;

    void Start()
    {
        // 지정한 lifetime 후에 현재 오브젝트(gameObject)를 파괴함
        Destroy(gameObject, lifetime);
    }
}
