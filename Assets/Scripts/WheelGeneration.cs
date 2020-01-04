using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGeneration : MonoBehaviour {

    [Header("Wheel Prefabs")]
    public GameObject wheelPrefab;

    [Header("Wheel Generation Settings")]
    public int wheelAmount;
    [Range(0.1f, 0.25f)]
    public float wheelLowerSpacingZ;
    [Range(0.1f, 0.25f)]
    public float wheelUpperSpacingZ;
    [Range(0f, -0.2f)]
    public float wheelUpperPosY;
    [Range(0f, -0.2f)]
    public float wheelLowerPosY;
    [Range(0.1f, 0.5f)]
    public float wheelOffsetX;


    void Start () {
        GenerateLowerWheels();
        GenerateUpperWheels();
        foreach(Transform wheel in GameObject.Find("Wheels").transform)
        {
            wheel.gameObject.AddComponent<WheelRotation>();
        }
    }

    void GenerateLowerWheels()
    {
        for (int i = -1; i < 3; i += 2)
        {
            float lowerWheelRangeZ = (wheelAmount - 4) / 4 * wheelLowerSpacingZ;
            float wheelPosX = wheelOffsetX * i;
            for (float z = -lowerWheelRangeZ; z <= lowerWheelRangeZ; z += wheelLowerSpacingZ)
            {
                Vector3 wheelPos = new Vector3(wheelPosX, wheelLowerPosY + transform.position.y, z);
                GameObject wheelObject = Instantiate(wheelPrefab, wheelPos, transform.rotation);
                if (wheelPosX > 0)
                    wheelObject.transform.Rotate(0, 180, 0);
                wheelObject.transform.parent = GameObject.Find("Wheels").transform;
                wheelObject.name = ("Lowerwheel " + wheelPos);
            }
        }
    }

    void GenerateUpperWheels()
    {
        for (int i = -1; i < 3; i += 2)
        {
            float upperWheelRangeZ = ((wheelAmount - 4) / 4 * wheelLowerSpacingZ) + wheelUpperSpacingZ;
            for (float z = -upperWheelRangeZ; z <= upperWheelRangeZ; z += upperWheelRangeZ * 2)
            {
                float wheelPosX = wheelOffsetX * i;
                Vector3 upperWheelPos = new Vector3(wheelPosX, wheelUpperPosY + transform.position.y, z);
                GameObject wheelObject = Instantiate(wheelPrefab, upperWheelPos, transform.rotation);
                if (wheelPosX > 0)
                    wheelObject.transform.Rotate(0, 180, 0);
                wheelObject.transform.parent = GameObject.Find("Wheels").transform;
                wheelObject.name = ("Upperwheel " + upperWheelPos);
            }
        }
    }
}
 
