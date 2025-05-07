using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    public bool simulateWithKeyboard = false;
    public float moveSpeed = 5f;
    public GameObject ballPrefab;
    public float throwForce = 10f;

    void Update()
    {
        if (simulateWithKeyboard)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float forward = 0f;
            if (Input.GetKey(KeyCode.E)) forward = 1f;
            if (Input.GetKey(KeyCode.Q)) forward = -1f;
            Vector3 delta = new Vector3(h, v, forward) * moveSpeed * Time.deltaTime;
            transform.Translate(delta, Space.World);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowBall();
            }
        }
        else
        {
            // 여기에 립모션 제스처 인식 코드 추가 예정
        }
    }

    public void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * throwForce;
    }
}
