using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingcontroller : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻�ü ������
    public Transform firePoint;          // �߻� ��ġ (�ѱ� ��ġ)

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
        // �߻�ü ���� �� �ʱ� ȸ�� ����
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}