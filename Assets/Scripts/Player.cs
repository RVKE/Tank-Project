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
        GetComponent<SpeedManager>().Move(Input.GetAxis("Vertical"));
        GetComponent<SpeedManager>().Rotate(Input.GetAxis("Horizontal"));
    }
}
