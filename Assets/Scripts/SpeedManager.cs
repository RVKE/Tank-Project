using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour {

    public float currentSpeed;
    public float currentRotationSpeed;
    private static float speedConversion = 20.0f;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * currentSpeed / speedConversion;
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * currentRotationSpeed);
    }
}
