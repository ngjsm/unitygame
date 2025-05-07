using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingcontroller : MonoBehaviour
{
    public GameObject projectilePrefab;  // 발사체 프리팹
    public Transform firePoint;          // 발사 위치 (총구 위치)

    void OnEnable()
    {
        LipMotion.OnLipOpen += Shoot;
    }

    void OnDisable()
    {
        LipMotion.OnLipOpen -= Shoot;
    }

    void Shoot()
    {
        // 발사체 생성 및 초기 회전 적용
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}