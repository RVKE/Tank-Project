using System.Collections;
using UnityEngine;

public enum DrivingState
{
    IDLE,
    MOVING,
}

public enum TurretState
{
    IDLE,
    ROTATING,
}

public class Player : MonoBehaviour {

    private PlayerState currentState;

    public GameObject turret;
    public GameObject cannon;
    public GameObject projectileEmitter;
    public GameObject projectile;

    private Camera cam;

    public float projectileSpeed;
    public float reloadCooldown;
    private bool fireReady = true;

    [Header("Movement Settings")]
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float maxRotationSpeed;
    public float turretDamping;

    void Start ()
    {
        cam = Camera.main;
    }

    void Update () {
        currentState = GameManager.instance.currentState;

        if (currentState == PlayerState.DRIVING) {
            SpeedControls();
            TurretControls();
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        } 
	}

    void SpeedControls()
    {
        //forward and backward

        float speed = GetComponent<SpeedManager>().currentSpeed;
        float forwardInput = Input.GetAxis("Vertical");
        if (forwardInput == 0)
        {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);
        }
        else if (speed < maxSpeed && speed > -maxSpeed/2)
        {
            speed += forwardInput * acceleration * Time.deltaTime;
        }
        GetComponent<SpeedManager>().currentSpeed = speed;

        //rotation

        float rotationSpeed = GetComponent<SpeedManager>().currentRotationSpeed;
        float rotationInput = Input.GetAxis("Horizontal");
        rotationSpeed = rotationInput * maxRotationSpeed * 10.0f * Time.deltaTime;
        GetComponent<SpeedManager>().currentRotationSpeed = rotationSpeed;
    }

    void TurretControls()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit;

        if (Physics.Raycast(ray, out rayhit))
        {
            Vector3 target = rayhit.point - turret.transform.position;
            target.y = 0;
            var rotation = Quaternion.LookRotation(target);
            turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, rotation, turretDamping * Time.deltaTime);
        }
    }

    void Fire()
    {
        if (fireReady == true)
        {
            //Fire
            GameObject projectileObject = Instantiate(projectile, projectileEmitter.transform.position, projectileEmitter.transform.rotation);
            Destroy(projectileObject, 25.0f);
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        fireReady = false;
        yield return new WaitForSeconds(reloadCooldown);
        fireReady = true;
    }
}
