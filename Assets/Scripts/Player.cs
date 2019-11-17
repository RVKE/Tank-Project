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

	void Start () {
		
	}
	
	void Update () {

        /*if (currentState == PlayerState.Inside) {
            Debug.Log("inside");
        } */

        if (Input.GetKeyDown(KeyCode.Tab)) {
            UpdatePlayerState();
        }
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
