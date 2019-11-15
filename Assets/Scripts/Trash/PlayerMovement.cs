using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float speed = 3.0f;

    private GameObject camObject;
    private Camera cam;

	void Start () {
        camObject = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camObject.GetComponent<Camera>();
	}
	
	void Update () {
        float translation = (Input.GetAxis("Horizontal")) * speed * Time.deltaTime;
        float straffe = (Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        transform.Translate(straffe, 0, -translation);

        camObject.transform.position = transform.position;
	}
}
