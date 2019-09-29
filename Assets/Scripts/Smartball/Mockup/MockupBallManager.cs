using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupBallManager : MonoBehaviour
{

    [SerializeField]
    MockupBaseBall ballPrefab;

    [SerializeField]
    Vector3 ballAddingPos;

    public static MockupBallManager instance;

    static List<MockupBaseBall> ballList;
    float lastAddingBallTime;
    static int ballAddingStackCount;

    void Awake()
    {
        instance = this;
        lastAddingBallTime = Time.time;
        ballList = new List<MockupBaseBall>();
    }

    void Update()
    {
        CreateBallIfNeeded();
    }

    public static void AddBall(int count)
    {
        ballAddingStackCount += count;
    }

    void CreateBallIfNeeded()
    {
        if (ballAddingStackCount == 0) { return; }
        if (Time.time - lastAddingBallTime < 0.5f) { return; }
        CreateBall();
        ballAddingStackCount -= 1;
    }

    void CreateBall()
    {
        GameObject clone = Instantiate<GameObject>(ballPrefab.gameObject, ballAddingPos, Quaternion.identity);
        ballList.Add(clone.GetComponent<MockupBaseBall>());
        lastAddingBallTime = Time.time;
    }

    public static void RemoveLastBallIfExist()
    {
        Debug.Log("RemoveLastBallIfExist() count:" + ballList.Count);
        if (ballList.Count == 0) { return; }
        Destroy(ballList[ballList.Count - 1].gameObject);
        ballList.RemoveAt(ballList.Count - 1);
    }

}
