using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class BallRemover : MonoBehaviour
{
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Ball") { return; }
        Destroy(coll.gameObject);
    }

}
