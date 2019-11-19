using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerState currentState;

    public float speed;
    public float rotateSpeed;

	void Update () {
        currentState = GameManager.instance.currentState;

        if (currentState == PlayerState.Driving) {
            PlayerMovement();
        } 
	}

    void PlayerMovement()
    {
        var transAmount = speed * Time.deltaTime * Input.GetAxis("Vertical");
        var rotateAmount = rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal");

        transform.Translate(0, 0, transAmount);
        transform.Rotate(0, rotateAmount, 0);

    }
}
