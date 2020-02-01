using UnityEditor;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

    #region Variables

    [Header("Target")]
    public Transform target;

    [Header("Positioning")]
    public float height;
    public float distance;
    public float angle;
    public float smoothingSpeed;

    private Vector3 velocity = Vector3.zero;

    #endregion

	void Update ()
    {
        HandleCamera();
	}

    protected void HandleCamera()
    {
        if (target)
        {
            Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;
            Vector3 flatTargetPosition = target.position;

            flatTargetPosition.y = 0;

            Vector3 finalPosition = flatTargetPosition + rotatedVector;

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothingSpeed);
            transform.LookAt(flatTargetPosition);
        }
    }
}
