using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballerinas : MonoBehaviour
{
    public Transform[] dancePoints;
    public Transform exitPoint;
    public Transform startPoint;
    public bool dance;
    public void BallerinaStartDance()
    {
        dance = true;
    }
}
