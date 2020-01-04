using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerState currentState;

    [Header("Movement Settings")]
    public float maxSpeed;
    public float acceleration;
    public float deceleration;

    public float maxRotationSpeed;

    void Update () {
        currentState = GameManager.instance.currentState;

        if (currentState == PlayerState.DRIVING) {
            SpeedControls();
        } 
	}

    void SpeedControls()
    {
        //forward and backward

        float speed = GetComponent<SpeedManager>().currentSpeed;
        float forwardInput = Input.GetAxis("Vertical");
        if (forwardInput == 0)
        {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);
        }
        else if (speed < maxSpeed && speed > -maxSpeed/2)
        {
            speed += forwardInput * acceleration * Time.deltaTime;
        }
        GetComponent<SpeedManager>().currentSpeed = speed;

        //rotation

        float rotationSpeed = GetComponent<SpeedManager>().currentRotationSpeed;
        float rotationInput = Input.GetAxis("Horizontal");
        rotationSpeed = rotationInput * maxRotationSpeed * 10.0f * Time.deltaTime;
        GetComponent<SpeedManager>().currentRotationSpeed = rotationSpeed;
    }
}
