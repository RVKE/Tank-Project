using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerState currentState;

	void Update () {
        currentState = GameManager.instance.currentState;

        if (currentState == PlayerState.Driving) {
            PlayerMovement();
        } 
	}

    void PlayerMovement()
    {
        if (!Input.GetKey("w"))
        {
            if (Input.GetKey("d"))
            {
                GetComponent<SpeedManager>().PowerToRightTrack(true);
            }
            else if (Input.GetKey("a"))
            {
                GetComponent<SpeedManager>().PowerToLeftTrack(true);
            } else
            {
                GetComponent<SpeedManager>().PowerToRightTrack(false);
                GetComponent<SpeedManager>().PowerToLeftTrack(false);
            }
        } else
        {
            GetComponent<SpeedManager>().PowerToRightTrack(true);
            GetComponent<SpeedManager>().PowerToLeftTrack(true);
        }
    }
}
