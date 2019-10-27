using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchableObject
{

    string objName { get; }
    void TouchBegin();
    void TouchMove();
    void TouchEnd();

}
