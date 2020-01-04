using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerState currentState;

    public float topSpeed;
    public float acceleration;
    public float rotateSpeed;

	void Update () {
        currentState = GameManager.instance.currentState;

        if (currentState == PlayerState.DRIVING) {
            PlayerMovement();
        } 
	}

    void PlayerMovement()
    {
        //player movement
    }
}
