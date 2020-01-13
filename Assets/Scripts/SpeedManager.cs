using UnityEngine;

public class SpeedManager : MonoBehaviour {

    public float maxSpeed;
    public float maxRotateSpeed;

    public float acceleration;

    public float currentSpeed;
    public float currentRotateSpeed;

    void Update() {
        transform.Rotate(new Vector3(0, currentRotateSpeed * Time.deltaTime, 0));
        transform.Translate(Vector3.forward * currentSpeed / maxSpeed * (maxSpeed / 5.0f) * Time.deltaTime);
    }

    public void Move(float forward)
    {
        currentSpeed = Mathf.Round(Mathf.Lerp(currentSpeed, maxSpeed * forward, acceleration) * 100.0f) / 100.0f;
    }

    public void Rotate(float forward)
    {
        currentRotateSpeed = maxRotateSpeed * forward;
    }
}
