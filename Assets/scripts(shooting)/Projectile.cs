using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [Header("�Ѿ� �Ӽ�")]
    [SerializeField] private float speed = 20f;      // �Ѿ� �̵� �ӵ�
    [SerializeField] private float lifetime = 2f;    // �Ѿ��� �����ϴ� �ð� (��)

    void Start()
    {
        // lifetime ���� �Ѿ��� �ڵ� �����Ͽ� �޸� ���� ����
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �Ѿ��� �ڽ��� forward �������� �̵��ϵ��� ��
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // ���� Target �±׸� üũ
        if (other.CompareTag("Target"))
        {
            if (score.Instance != null)
                score.Instance.AddTargetHit();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // �� ���� Fruit �±��� ��� ó��
        else if (other.CompareTag("Fruit"))
        {
            if (score.Instance != null)
                score.Instance.AddOtherHit();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // �÷��̾ �ƴ� �ٸ� ������Ʈ�� �浹 �� �Ѿ˸� ����
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}