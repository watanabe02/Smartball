using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{

    Hole m_Hole;

    public Hole hole { set { m_Hole = value; } }

    void OnTriggerEnter()
    {
        if (m_Hole == null) { return; }
        m_Hole.OnHolein();
    }

}
