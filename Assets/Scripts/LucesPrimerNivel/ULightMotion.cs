using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULightMotion : MonoBehaviour
{
    [SerializeField] Vector2 u_cycleDurationXZ = new Vector2(20f, 20f);
    [SerializeField] AnimationCurve u_movementPathX;
    [SerializeField] AnimationCurve u_movementPathZ;
    [SerializeField] Vector2 u_movementMagnitudeXZ = new Vector2(1, 1);
    [SerializeField] Vector2 u_movementTimeOffsetXZ = new Vector2();

    private Vector3 u_initialPosition;


    private void Awake()
    {
        u_initialPosition = transform.position;
    }


    void Update()
    {
        UpdateUotion();
    }


    private void UpdateUotion()
    {
        float timeX = Time.time % u_cycleDurationXZ.x;
        timeX /= u_cycleDurationXZ.x;

        float timeZ = Time.time % u_cycleDurationXZ.y;
        timeZ /= u_cycleDurationXZ.y;

        float newX = u_movementPathX.Evaluate(timeX + u_movementTimeOffsetXZ.x) * u_movementMagnitudeXZ.x;
        float newZ = u_movementPathZ.Evaluate(timeZ + u_movementTimeOffsetXZ.y) * u_movementMagnitudeXZ.y;

        transform.position = u_initialPosition + new Vector3(newX, 0, newZ);
    }
}