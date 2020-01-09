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
        if (Input.GetKeyDown("w"))
        {
            GetComponent<SpeedManager>().PowerToLeftTrack();
            GetComponent<SpeedManager>().PowerToRightTrack();
        }
        else if (Input.GetKeyDown("d"))
        {
            GetComponent<SpeedManager>().PowerToRightTrack();
        }
        else if (Input.GetKeyDown("a"))
        {
            GetComponent<SpeedManager>().PowerToLeftTrack();
        }
    }
}
