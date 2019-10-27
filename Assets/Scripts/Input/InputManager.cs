using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    static InputManager m_Instance;
    Camera m_MainCamera;
    List<ITouchableObject> m_CurrentTouchBeginObjList = new List<ITouchableObject>();
    List<ITouchableObject> m_CurrentTouchMoveObjList = new List<ITouchableObject>();
    List<ITouchableObject> m_CurrentTouchEndObjList = new List<ITouchableObject>();
    List<ITouchableObject> m_LastTouchBeginObjList = new List<ITouchableObject>();
    List<ITouchableObject> m_LastTouchMoveObjList = new List<ITouchableObject>();
    List<ITouchableObject> m_LastTouchEndObjList = new List<ITouchableObject>();

    public static List<ITouchableObject> touchBeginObjList { get { return m_Instance.m_LastTouchBeginObjList; } }
    public static List<ITouchableObject> touchMoveObjList { get { return m_Instance.m_LastTouchMoveObjList; } }
    public static List<ITouchableObject> touchEnfObjList { get { return m_Instance.m_LastTouchEndObjList; } }


    void Awake()
    {
        m_Instance = this;
        m_MainCamera = Camera.main;
    }

    void Update()
    {
        m_CurrentTouchBeginObjList.Clear();
        m_CurrentTouchMoveObjList.Clear();
        m_CurrentTouchEndObjList.Clear();

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            ITouchableObject obj = GetTouchableObject(Input.mousePosition);
            if (obj != null)
            {
                m_CurrentTouchBeginObjList.Add(obj);
                obj.TouchBegin();
            }
        }
#else
        int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            ITouchableObject obj = GetTouchableObject(touch.position);
            if (obj == null) { continue; }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    m_CurrentTouchBeginObjList.Add(obj);
                    obj.TouchBegin();
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    m_CurrentTouchMoveObjList.Add(obj);
                    obj.TouchMove();
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    m_CurrentTouchEndObjList.Add(obj);
                    obj.TouchEnd();
                    break;
            }
        }
#endif

        m_LastTouchBeginObjList = m_CurrentTouchBeginObjList;
        m_LastTouchMoveObjList = m_CurrentTouchMoveObjList;
        m_LastTouchEndObjList = m_CurrentTouchEndObjList;
    }

    ITouchableObject GetTouchableObject(Vector2 point)
    {
        Ray ray = m_MainCamera.ScreenPointToRay(point);
        RaycastHit hit;
        float maxDistance = 1f;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            return hit.collider.GetComponent<ITouchableObject>();
        }
        else
        {
            return null;
        }
    }


}
