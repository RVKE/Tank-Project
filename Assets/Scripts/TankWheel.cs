using UnityEngine;

public class TankWheel : MonoBehaviour {

    [Header("Suspension Settings")]
    private bool enableAlignment = false;
    private float raycastOffsetY = 0.0f;
    private float adjustmentDamping = 0.2f;
    private float maxHeight = 0.1f;
    private float alignmentFactor = 4.0f;

    private Vector3 raycastOrigin;
    private Vector3 newWheelPosition;
    private RaycastHit hit;

    void Update()
    {
        raycastOrigin = transform.position - new Vector3(0, raycastOffsetY, 0);
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, Mathf.Infinity))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hit.point.y + transform.localScale.z / 2, transform.position.z), adjustmentDamping);
        }
        Debug.DrawRay(raycastOrigin, Vector3.down, Color.red);
        
        if (enableAlignment)
        {
            foreach (GameObject wheel in GameObject.FindGameObjectsWithTag("Wheel"))
            {
                if (wheel.transform.position.y > transform.position.y + maxHeight)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, maxHeight / alignmentFactor, 0), adjustmentDamping * 10);
                }
            }
        }
    }
}
