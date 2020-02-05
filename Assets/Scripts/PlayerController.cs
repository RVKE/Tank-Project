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
    public float reticleSmoothingSpeed;
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
            Vector3 turretDirection = input.mouseWorldPosition - turretTransform.position;
            turretDirection.y = 0f;

            finalTurretDirection = Vector3.Lerp(finalTurretDirection, turretDirection, turretSmoothingSpeed * Time.deltaTime);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretDirection);
        }
    }

    protected void HandleReticle()
    {
        if (reticleTransform)
        {
            Vector3 reticlePosition = new Vector3(input.mouseWorldPosition.x, 0.5f, input.mouseWorldPosition.z);
            reticleTransform.position = Vector3.Lerp(reticleTransform.position, reticlePosition, reticleSmoothingSpeed);
        }
    }

    protected void HandlePlayerState()
    {
        if (currentPlayerState == PlayerState.COMMANDING)
        {
            mainCamera.gameObject.SetActive(true);
            outsideGroup.SetActive(!outsideGroup.activeInHierarchy);
            insideGroup.SetActive(!outsideGroup.activeInHierarchy);
            mainCamera.transform.position = turretTransform.position;
            RenderSettings.ambientLight = Color.white;
            currentPlayerState = PlayerState.DRIVING;
        }
        else
        {
            mainCamera.gameObject.SetActive(false);
            outsideGroup.SetActive(!outsideGroup.activeInHierarchy);
            insideGroup.SetActive(!outsideGroup.activeInHierarchy);
            commandCamera.transform.rotation = commandCamera.transform.rotation * Quaternion.Euler(80, 0, 0);
            RenderSettings.ambientLight = Color.black;
            currentPlayerState = PlayerState.COMMANDING;
        }
    }
}
