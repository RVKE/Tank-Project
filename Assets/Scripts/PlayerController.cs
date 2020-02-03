using UnityEngine;

public enum PlayerState
{
    COMMANDING,
    DRIVING,
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    #region Variables
    [Header("Tank Gameobjects")]
    public GameObject outsideGroup;
    public GameObject insideGroup;

    [Header("Player State")]
    public PlayerState currentPlayerState;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera commandCamera;

    [Header("Movement")]
    public float tankSpeed;
    public float tankRotationSpeed;

    [Header("Turret")]
    public Transform turretTransform;

    [Header("Reticle")]
    public Transform reticleTransform;
    public float turretSmoothingSpeed;


    private Rigidbody rigidBody;
    private PlayerInput input;
    private Vector3 finalTurretDirection;

    #endregion

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        currentPlayerState = PlayerState.DRIVING;
    }
	
	void Update () {
		if (rigidBody && input && currentPlayerState == PlayerState.DRIVING)
        {
            HandleMovement();
            HandleTurret();
            HandleReticle();
        }
        if (input.tabKeyInput)
            HandlePlayerState();
    }

    protected void HandleMovement()
    {
        //Forward
        Vector3 wantedPosition = transform.position + (transform.forward * input.forwardInput * tankSpeed * Time.deltaTime);
        rigidBody.MovePosition(wantedPosition);

        //Rotate
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.rotationInput * Time.deltaTime));
        rigidBody.MoveRotation(wantedRotation);
    }

    protected void HandleTurret()
    {
        if (turretTransform)
        {
            Vector3 turretDirection = input.reticlePosition - turretTransform.position;
            turretDirection.y = 0f;

            finalTurretDirection = Vector3.Lerp(finalTurretDirection, turretDirection, turretSmoothingSpeed * Time.deltaTime);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretDirection);
        }
    }

    protected void HandleReticle()
    {
        if (reticleTransform)
        {
            reticleTransform.position = input.reticlePosition;
        }
    }

    protected void HandlePlayerState()
    {
        if (currentPlayerState == PlayerState.COMMANDING)
        {
            outsideGroup.SetActive(!outsideGroup.activeInHierarchy);
            insideGroup.SetActive(!outsideGroup.activeInHierarchy);
            mainCamera.transform.position = turretTransform.position;
            currentPlayerState = PlayerState.DRIVING;
        }
        else
        {
            outsideGroup.SetActive(!outsideGroup.activeInHierarchy);
            insideGroup.SetActive(!outsideGroup.activeInHierarchy);
            commandCamera.transform.rotation = Quaternion.Euler(90, 90, 0);
            currentPlayerState = PlayerState.COMMANDING;
        }
    }
}
