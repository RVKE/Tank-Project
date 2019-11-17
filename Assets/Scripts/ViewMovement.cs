using UnityEngine;

public class ViewMovement : MonoBehaviour {

    private GameObject camObject;
    private Camera cam;
    private GameObject player;

    void Start()
    {
        camObject = Camera.main.gameObject;
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    void DebugMovement()
    {
        float translation = (Input.GetAxis("Vertical")) * 10.0f * Time.deltaTime;
        float straffe = (Input.GetAxis("Horizontal")) * 10.0f * Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }
}