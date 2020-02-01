using UnityEngine;

public class WheelGeneration : MonoBehaviour {

    public GameObject trackObject;

    private GameObject modelParent;

    [Header("Wheel Prefab")]
    public GameObject wheelPrefab;

    [Header("Wheel Generation Settings")]
    public int wheelAmount;
    [Range(0.1f, 1.0f)]
    public float wheelLowerSpacingZ;
    [Range(0.1f, 1.0f)]
    public float wheelUpperSpacingZ;
    [Range(-0.2f, 0.2f)]
    public float wheelUpperPosY;
    [Range(-0.2f, -0.0f)]
    public float wheelLowerPosY;
    [Range(0.1f, 1.6f)]
    public float wheelOffsetX;

    void Start () {
        modelParent = transform.Find("Model").gameObject;
    
        for (float i = -wheelOffsetX; i <= wheelOffsetX; i += wheelOffsetX * 2)
        {
            GameObject track = Instantiate(trackObject, new Vector3(i, 1, 0), transform.rotation);
            track.transform.parent = modelParent.transform;
            track.transform.name = "Track";
        }
        foreach (Transform child in modelParent.transform)
        {
            if (child.name == "Track")
            {
                GenerateLowerWheels(child);
                GenerateUpperWheels(child);
                child.gameObject.AddComponent<TankTrack>();
            }
        }

    }

    void GenerateUpperWheels(Transform parent)
    {
        float upperWheelRangeZ = ((wheelAmount - 4) / 4 * wheelLowerSpacingZ) + wheelUpperSpacingZ;
        for (float z = -upperWheelRangeZ; z <= upperWheelRangeZ; z += upperWheelRangeZ * 2)
        {
            GameObject wheelObject = Instantiate(wheelPrefab, new Vector3(parent.gameObject.transform.position.x, wheelUpperPosY + transform.position.y, z), transform.rotation);
            wheelObject.transform.parent = parent;
            wheelObject.name = "Upper Wheel (" + z + ")";
            if (wheelObject.transform.position.x > 0)
                wheelObject.transform.Rotate(0, 180, 0);
        }
    }

    void GenerateLowerWheels(Transform parent)
    {
        float lowerWheelRangeZ = (wheelAmount - 4) / 4 * wheelLowerSpacingZ;
        for (float z = -lowerWheelRangeZ; z <= lowerWheelRangeZ; z += wheelLowerSpacingZ)
        {
            GameObject wheelObject = Instantiate(wheelPrefab, new Vector3(parent.gameObject.transform.position.x, parent.gameObject.transform.position.y + wheelLowerPosY, z), transform.rotation);
            wheelObject.transform.parent = parent;
            wheelObject.name = "Lower Wheel (" + z + ")";
            wheelObject.AddComponent<TankWheel>();
            if (wheelObject.transform.position.x > 0)
                wheelObject.transform.Rotate(0, 180, 0);
        }
    }
}
