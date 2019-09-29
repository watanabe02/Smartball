using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupBallLauncer : MonoBehaviour
{

    [SerializeField]
    MockupBaseBall testBall;

    //[SerializeField]
    Vector3 launchPos;

    [SerializeField]
    float launchAcc;

    [SerializeField]
    float randAccRange;



    void Awake()
    {
        launchPos = transform.position;
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        float rand = UnityEngine.Random.value;
        float range = Mathf.Lerp(-randAccRange, randAccRange, rand);
        Vector3 force = new Vector3(0f, 0f, launchAcc + range);
        testBall.ballRigitbody.velocity = Vector3.zero;
        testBall.ballRigitbody.AddForce(force);
        testBall.transform.rotation = Quaternion.identity;
        testBall.transform.position = launchPos;

        MockupBallManager.RemoveLastBallIfExist();
    }


}
