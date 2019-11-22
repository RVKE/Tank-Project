using UnityEngine;

public class ViewMovement : MonoBehaviour {

    private GameObject camObject;
    private Camera cam;
    private GameObject player;

    public bool enableDebugMode;
    public float debugSpeed;

    void Start()
    {
        camObject = Camera.main.gameObject;
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");

        if (enableDebugMode)
        {
            player.transform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (player.activeInHierarchy == true)
            transform.position = player.transform.position;
        if (enableDebugMode)
            DebugMovement();

    }

    void DebugMovement()
    {
        float translation = (Input.GetAxis("Vertical")) * debugSpeed * Time.deltaTime;
        float straffe = (Input.GetAxis("Horizontal")) * debugSpeed * Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }
}