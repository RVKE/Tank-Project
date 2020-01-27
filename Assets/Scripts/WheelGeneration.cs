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
    [Range(0f, -0.2f)]
    public float wheelUpperPosY;
    [Range(0f, -0.2f)]
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
                GenerateUpperWheels();
            }
        }

    }

    void GenerateUpperWheels()
    {

    }

    void GenerateLowerWheels(Transform parent)
    {
        for(float i = -(wheelAmount / 4); i <= wheelAmount / 4; i += wheelLowerSpacingZ)
        {
            GameObject wheelObject = Instantiate(wheelPrefab, new Vector3(parent.gameObject.transform.position.x,  parent.gameObject.transform.position.y + wheelLowerPosY, i), transform.rotation);
            wheelObject.transform.parent = parent;
            wheelObject.name = "Lower Wheel (" + i + ")";
            wheelObject.AddComponent<TankWheel>();
            if (wheelObject.transform.position.x > 0)
                wheelObject.transform.Rotate(0, 180, 0);
        }
    }

    void Update () {
		
	}
}
