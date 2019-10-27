using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour, ITouchableObject
{

    public string objName { get { return gameObject.name; } }
    public void TouchBegin()
    {
        BallManager.DestroyAllBall();
        BallLoader.AddBall(45);
    }
    public void TouchMove() { }
    public void TouchEnd() { }

}
