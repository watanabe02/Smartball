using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLoader : MonoBehaviour
{
    
    [SerializeField] Transform m_BottomPosT;
    [SerializeField] Transform m_RaycastT;


    static BallLoader m_Instance;
    Queue<int> m_AddingBallTask = new Queue<int>();
    const float m_RaycastTimeInterval = 0.1f;
    const float m_BallInterval = 0.001f;
    float m_LastRaycastTime;
    const int m_MaxLoadingBallCount = 5;


    public static BallLoader insatnce { get { return m_Instance; } }


    void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        AddBall(20);
    }

    void Update()
    {
        if (m_AddingBallTask.Count == 0) { return; }
        if (IsBallInLoadingSpace()) { return; }
        InstallBall(m_AddingBallTask.Dequeue());
    }

    public static void AddBall(int ballCount)
    {
        int count = ballCount;
        while (count > m_MaxLoadingBallCount)
        {
            m_Instance.m_AddingBallTask.Enqueue(m_MaxLoadingBallCount);
            count -= m_MaxLoadingBallCount;
        }
        m_Instance.m_AddingBallTask.Enqueue(count);
    }

    bool IsBallInLoadingSpace()
    {
        if (Time.time - m_LastRaycastTime < m_RaycastTimeInterval)
        {
            return true;
        }

        Ray ray = new Ray(m_RaycastT.position, -m_RaycastT.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (hit.collider.tag == "Ball")
            {
                return true;
            }
        }
        return false;
    }

    void InstallBall(int ballCount)
    {
        float ballDiameter = BallManager.ballDiameter;
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 ballPos = m_BottomPosT.position + (m_BallInterval + ballDiameter) * i * m_BottomPosT.up;
            BallManager.CreateBall(ballPos);
        }
        m_LastRaycastTime = Time.time;
    }



}
