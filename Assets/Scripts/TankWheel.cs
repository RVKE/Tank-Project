using UnityEngine;

public class TankWheel : MonoBehaviour {

    [Header("Suspension Settings")]
    private float raycastOffsetY = 0.0f;
    private float adjustmentDamping = 0.2f;

    private float wheelDiameter;

    private Vector3 raycastOrigin;
    private Vector3 newWheelPosition;
    private RaycastHit hit;

    void Start()
    {
        wheelDiameter = GetComponent<MeshFilter>().transform.localScale.y;
    }

    void Update()
    {
        raycastOrigin = transform.position - new Vector3(0, raycastOffsetY, 0);
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, Mathf.Infinity))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hit.point.y + wheelDiameter / 2, transform.position.z), adjustmentDamping);
        }

        Debug.DrawRay(raycastOrigin, Vector3.down, Color.red);
    }
}
