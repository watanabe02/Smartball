using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public enum BallState
    {
        InPool,
        InBoard
    }

    [SerializeField]
    Rigidbody m_Rigidbody;

    /// <summary>
    /// This value is square.
    /// </summary>
    const float m_HitSfxVelThreshold = 0.05f;
    const float m_SfxIntarvalTime = 0.2f;
    float m_LastPlaySfxTime;
    BallState m_BallState = BallState.InPool;

    public BallState ballState { get { return m_BallState; } set { m_BallState = value; } }

    void OnCollisionEnter(Collision coll)
    {
        if (Time.time - m_LastPlaySfxTime < m_SfxIntarvalTime)
        {
            return;
        }
        
        if (coll.relativeVelocity.sqrMagnitude < m_HitSfxVelThreshold)
        {
            return;
        }

        if (m_Rigidbody.velocity.sqrMagnitude < m_HitSfxVelThreshold)
        {
            return;
        }

        Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            PlaySfx(coll.gameObject.tag);
        }
        else if (m_Rigidbody.velocity.sqrMagnitude >= rb.velocity.sqrMagnitude)
        {
            SfxManager.Play(SfxName.Hit);
        }
        else
        {
            return;
        }

        m_LastPlaySfxTime = Time.time;
    }

    void PlaySfx(string tag)
    {
        switch (tag)
        {
            case "NonSfx":
                break;
            case "Spring":
                SfxManager.Play(SfxName.Spring);
                break;
            default:
                SfxManager.Play(SfxName.Hit);
                break;
        }
    }

}
