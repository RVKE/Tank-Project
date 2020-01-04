using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	void Update () {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f * Time.deltaTime);
    }
}
