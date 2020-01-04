using UnityEngine;

public class SpeedManager : MonoBehaviour {

    public DrivingState currentState;

    public float currentSpeed;
    public float currentRotationSpeed;
    private static float speedConversion = 20.0f;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * currentSpeed / speedConversion;
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * currentRotationSpeed);

        if (currentSpeed != 0 || currentRotationSpeed != 0)
            currentState = DrivingState.MOVING;
        else
            currentState = DrivingState.IDLE;
    }
}
