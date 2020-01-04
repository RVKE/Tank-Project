using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

	void Update () {
        float wheelSpeed = player.GetComponent<SpeedManager>().currentSpeed;
        gameObject.transform.Rotate(Vector3.left * (wheelSpeed * 20.0f * Time.deltaTime));
    }
}
