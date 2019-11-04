using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public enum GateState
    {
        Closed,
        Opened
    }

    [SerializeField]
    int m_AddingBallCount = 5;

    [SerializeField]
    HoleCollider m_HoleColl;

    [SerializeField]
    TextMesh m_NumTextMesh;

    [SerializeField]
    Transform m_RightGateT;

    [SerializeField]
    Transform m_LeftGateT;


    static List<Hole> m_GateHoleList = new List<Hole>();
    Coroutine m_GateCoroutne;
    const float m_ClosedAngleY = 180.0f;
    const float m_OpenedAngleY = 58.0f;

    /// <summary>
    /// degree per sec.
    /// </summary>
    const float m_GateRotationSpeed = 180.0f;
    const int m_MaxOpenedGateCount = 1;
    bool m_IsGatePin = false;
    GateState m_GateState = GateState.Closed;

    public GateState gateState { get { return m_GateState; } }

    void Awake()
    {
        m_NumTextMesh.text = m_AddingBallCount.ToString();
        m_HoleColl.hole = this;
        m_IsGatePin = m_RightGateT.gameObject.activeSelf;
        if (m_IsGatePin)
        {
            m_RightGateT.transform.localEulerAngles = new Vector3(0, m_ClosedAngleY, 0);
            m_LeftGateT.transform.localEulerAngles = new Vector3(0, m_ClosedAngleY, 0);
            m_GateHoleList.Add(this);
            if (m_GateHoleList.Count <= m_MaxOpenedGateCount)
            {
                OpenGate();
            }
        }
    }

    public void OnHolein()
    {
        BallLoader.AddBall(m_AddingBallCount);
        SfxManager.Play(SfxName.Holein);
        OpenAnotherGateIfNeeded();
    }

    void OpenAnotherGateIfNeeded()
    {
        if (m_IsGatePin == false) { return; }
        if (m_GateState == GateState.Closed) { return; }
        int opendGateCount = -1;
        for (int i = 0; i < m_GateHoleList.Count; i++)
        {
            if (m_GateHoleList[i].gateState == GateState.Closed)
            { continue; }
            opendGateCount += 1;
            if (opendGateCount >= m_MaxOpenedGateCount) { return; }
        }

        List<Hole> closedHoleList = new List<Hole>();
        for (int i = 0; i < m_GateHoleList.Count; i++)
        {
            if (m_GateHoleList[i].gateState == GateState.Opened)
            { continue; }
            closedHoleList.Add(m_GateHoleList[i]);
        }
        int gateIndex = UnityEngine.Random.RandomRange(0, closedHoleList.Count);
        closedHoleList[gateIndex].OpenGate();
        CloseGate();
    }

    void OpenGate()
    {
        m_GateState = GateState.Opened;
        if (m_GateCoroutne != null) { StopCoroutine(m_GateCoroutne); }
        m_GateCoroutne = StartCoroutine(OpenGateCoroutine());
    }

    void CloseGate()
    {
        m_GateState = GateState.Closed;
        if (m_GateCoroutne != null) { StopCoroutine(m_GateCoroutne); }
        m_GateCoroutne = StartCoroutine(CloseGateCoroutine());
    }

    IEnumerator OpenGateCoroutine()
    {
        Vector3 rEA = m_RightGateT.localEulerAngles;
        Vector3 lEA = m_LeftGateT.localEulerAngles;
        while (rEA.y != m_OpenedAngleY || lEA.y != 360 - m_OpenedAngleY)
        {
            if (rEA.y != m_OpenedAngleY)
            {
                rEA.y -= m_GateRotationSpeed * Time.deltaTime;
                if (m_OpenedAngleY - rEA.y > 0.0f)
                {
                    rEA.y = m_OpenedAngleY;
                }
                m_RightGateT.localEulerAngles = rEA;
            }

            if (lEA.y != 360 - m_OpenedAngleY)
            {
                lEA.y += m_GateRotationSpeed * Time.deltaTime;
                if ((360 - m_OpenedAngleY) - lEA.y < 0.0f)
                {
                    lEA.y = 360 - m_OpenedAngleY;
                }
                m_LeftGateT.localEulerAngles = lEA;
            }
            yield return null;
        }
        m_GateCoroutne = null;
    }

    IEnumerator CloseGateCoroutine()
    {
        Vector3 rEA = m_RightGateT.localEulerAngles;
        Vector3 lEA = m_LeftGateT.localEulerAngles;
        while (rEA.y != m_ClosedAngleY || lEA.y != 360 - m_ClosedAngleY)
        {
            if (rEA.y != m_ClosedAngleY)
            {
                rEA.y += m_GateRotationSpeed * Time.deltaTime;
                if (m_ClosedAngleY - rEA.y < 0.0f)
                {
                    rEA.y = m_ClosedAngleY;
                }
                m_RightGateT.localEulerAngles = rEA;
            }

            if (lEA.y != 360 - m_ClosedAngleY)
            {
                lEA.y -= m_GateRotationSpeed * Time.deltaTime;
                if ((360 - m_ClosedAngleY) - lEA.y > 0.0f)
                {
                    lEA.y = 360 - m_ClosedAngleY;
                }
                m_LeftGateT.localEulerAngles = lEA;
            }
            yield return null;
        }
        m_GateCoroutne = null;
    }

}
