using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Inside,
    Outside,
}

public class Player : MonoBehaviour {

    public PlayerState currentState;

    public float speed;
    public float rotateSpeed;

	void Start () {
        currentState = PlayerState.Outside;
    }
	
	void Update () {

        if (currentState == PlayerState.Outside) {
            PlayerMovement();
        } 

        if (Input.GetKeyDown(KeyCode.Tab)) {
            UpdatePlayerState();
        }
	}

    void PlayerMovement()
    {
        var transAmount = speed * Time.deltaTime * Input.GetAxis("Vertical");
        var rotateAmount = rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal");

        transform.Translate(0, 0, transAmount);
        transform.Rotate(0, rotateAmount, 0);

    }


    void UpdatePlayerState()
    {
        if (currentState == PlayerState.Inside)
            currentState = PlayerState.Outside;
        else
            currentState = PlayerState.Inside;

        switch (currentState)
        {
            case PlayerState.Inside:
                Debug.Log("inside mode");
                break;
            case PlayerState.Outside:
                Debug.Log("outside mode");
                break;
        }
    }
}
