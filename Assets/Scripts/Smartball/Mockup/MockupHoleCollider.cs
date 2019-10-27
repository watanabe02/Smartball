using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupHoleCollider : MonoBehaviour
{
    [SerializeField]
    int addingCount = 1;


    void OnTriggerEnter()
    {
        Debug.Log("hole in!! count:" + addingCount);
        BallLoader.AddBall(addingCount);
    }



}
