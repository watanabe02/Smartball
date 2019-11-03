using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupHoleCollider : MonoBehaviour
{
    [SerializeField]
    int addingCount = 1;


    void OnTriggerEnter()
    {
        BallLoader.AddBall(addingCount);
        SfxManager.Play(SfxName.Holein);
    }



}
