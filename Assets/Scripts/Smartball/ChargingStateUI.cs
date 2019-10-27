using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingStateUI : MonoBehaviour
{

    [SerializeField] List<SpriteRenderer> m_SpriteRenList;


    static ChargingStateUI m_Instance;

    


    void Awake()
    {
        m_Instance = this;
        SetChargingState(0.0f);
    }

    public static void SetChargingState(float rate)
    {
        int floor = Mathf.Clamp((int)Mathf.Floor(rate * 10.0f), 0, m_Instance.m_SpriteRenList.Count - 1);
        Debug.Log("floor:" + floor);
        for (int i = 0; i < floor; i++)
        {
            m_Instance.m_SpriteRenList[i].color = Color.white;
        }

        m_Instance.m_SpriteRenList[floor].color = Color.white * rate;

        int ceil = Mathf.Clamp((int)Mathf.Ceil(rate * 10.0f), 0, m_Instance.m_SpriteRenList.Count - 1);
        Debug.Log("ceil:" + ceil);
        for (int i = ceil; i < m_Instance.m_SpriteRenList.Count; i++)
        {
            m_Instance.m_SpriteRenList[i].color = Color.clear;
        }
    }

}
