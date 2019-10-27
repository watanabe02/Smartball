using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] Transform m_MuzzleT;

    [SerializeField] float m_LaunchForce = 5.0f;
    [SerializeField] float m_MaxChargingRange = 10.0f;
    [SerializeField] float m_MaxForce = 3.0f;


    Ball m_ChamberBall;
    Vector2 m_MouseClickPos = Vector2.zero;
    float m_ChargingRate;
    bool m_IsCharging;


    void OnTriggerEnter(Collider other)
    {
        m_ChamberBall = other.GetComponent<Ball>();

    }

    void Awake()
    {
        m_MaxChargingRange = Screen.height * 0.5f;
        Debug.Log("m_MaxChargingRange:" + m_MaxChargingRange);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LaunchBall(m_LaunchForce);
        }
        CheckMouseState();
    }

    void LaunchBall(float force)
    {
        if (m_ChamberBall == null) {
            Debug.Log("ball is null!!");
            return; }
        m_ChamberBall.transform.position = m_MuzzleT.position;
        Rigidbody rb = m_ChamberBall.GetComponent<Rigidbody>();
        rb.AddForce(m_MuzzleT.forward * force);
        m_ChamberBall = null;
    }

    void CheckMouseState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_MouseClickPos = Input.mousePosition;
            m_IsCharging = true;
        }
        else if (Input.GetMouseButton(0))
        {
            float diffY = m_MouseClickPos.y - Input.mousePosition.y;
            diffY = Mathf.Clamp(diffY, 0.0f, m_MaxChargingRange);
            m_ChargingRate = diffY / m_MaxChargingRange;
            ChargingStateUI.SetChargingState(m_ChargingRate);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LaunchBall(m_MaxForce * m_ChargingRate);
            ChargingStateUI.SetChargingState(0.0f);
        }
    }

}
