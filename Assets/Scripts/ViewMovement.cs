﻿using UnityEngine;

public class ViewMovement : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        //transform.Translate(Vector3.forward * speed);
        float translation = (Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        float straffe = (Input.GetAxis("Horizontal")) * speed * Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }
}