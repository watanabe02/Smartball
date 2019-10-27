using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    [SerializeField] GameObject m_BallPrefab;

    static BallManager m_Instance;
    List<Ball> m_BallList = new List<Ball>();

    public static BallManager instance { get { return m_Instance; } }
    public static float ballDiameter { get { return m_Instance.m_BallPrefab.transform.localScale.x; } }

    void Awake()
    {
        m_Instance = this;
    }

    public static void CreateBall(Vector3 worldPos)
    {
        GameObject newBallObj = Instantiate<GameObject>(m_Instance.m_BallPrefab, worldPos, Quaternion.identity, m_Instance.transform);
        m_Instance.m_BallList.Add(newBallObj.GetComponent<Ball>());
    }

}
