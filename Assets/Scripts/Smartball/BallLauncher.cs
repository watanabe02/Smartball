using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] Transform m_MuzzleT;

    [SerializeField] float m_LaunchForce = 5.0f;


    Ball m_ChamberBall;


    void OnTriggerEnter(Collider other)
    {
        m_ChamberBall = other.GetComponent<Ball>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (m_ChamberBall == null) {
            Debug.Log("ball is null!!");
            return; }
        m_ChamberBall.transform.position = m_MuzzleT.position;
        Rigidbody rb = m_ChamberBall.GetComponent<Rigidbody>();
        rb.AddForce(m_MuzzleT.forward * m_LaunchForce);
        m_ChamberBall = null;
    }

}
