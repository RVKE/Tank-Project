using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Commanding,
    Driving,
}

public class Player : MonoBehaviour {

    public PlayerState currentState;

    public float speed;
    public float rotateSpeed;

	void Start () {
        currentState = PlayerState.Driving;
    }
	
	void Update () {

        if (currentState == PlayerState.Driving) {
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
        if (currentState == PlayerState.Commanding)
            currentState = PlayerState.Driving;
        else
            currentState = PlayerState.Commanding;

        switch (currentState)
        {
            case PlayerState.Commanding:
                Debug.Log(currentState);
                break;
            case PlayerState.Driving:
                Debug.Log(currentState);
                break;
        }
    }
}
