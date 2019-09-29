using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupBaseBall : MonoBehaviour
{

    [SerializeField]
    public Rigidbody ballRigitbody;


    [SerializeField]
    public Vector3 startPos;

    [SerializeField]
    public float thresholdLocalPosPosY;


    void Awake()
    {
        startPos = transform.localPosition;
        ballRigitbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (IsBoardOut())
        {
            ResetPosition();
        }
    }

    bool IsBoardOut()
    {
        return false;
        //return transform.localPosition.y < thresholdLocalPosPosY;
    }

    void ResetPosition()
    {
        transform.localPosition = startPos;
    }
    
}
