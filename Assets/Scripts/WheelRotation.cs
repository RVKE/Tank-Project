using UnityEngine;

public class WheelRotation : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

	void Update () {
        float speed = player.GetComponent<SpeedManager>().currentSpeed;
        float rotationSpeed = player.GetComponent<SpeedManager>().currentRotationSpeed;
        if (speed == 0)
        {
            if (rotationSpeed > 0)
            {
                if (gameObject.transform.tag == "LeftWheel")
                    RotateWheel(-20.0f);
                else
                    RotateWheel(-20.0f);
            } else if (rotationSpeed < 0)
            {
                if (gameObject.transform.tag == "LeftWheel")
                    RotateWheel(20.0f);
                else
                    RotateWheel(20.0f);
            }
        } else
        {
            if (gameObject.transform.tag == "LeftWheel")
                RotateWheel(-speed);
            else
                RotateWheel(speed);
        }
    }

    void RotateWheel(float wheelSpeed)
    {
        gameObject.transform.Rotate(Vector3.left * (wheelSpeed * 20.0f * Time.deltaTime));
    }
}
