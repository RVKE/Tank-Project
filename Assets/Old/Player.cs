/*

using UnityEngine;

public class Player : MonoBehaviour {
    private PlayerState currentState;

    [Header("Reticle")]
    public Transform reticleTransform;

	void Update () {

        if (currentState == PlayerState.DRIVING) {
            TankMovement();
            TurretMovement();
        } 
	}

    void TurretMovement()
    {
        HandleReticle();
    }

    void TankMovement()
    {
        GetComponent<SpeedManager>().Move(Input.GetAxis("Vertical"));
        GetComponent<SpeedManager>().Rotate(Input.GetAxis("Horizontal"));
    }

    void HandleReticle()
    {
        if (reticleTransform)
        {
            //reticleTransform.position = Input.R
        }
    }
}
*/