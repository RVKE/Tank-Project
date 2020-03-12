using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TankTrack : MonoBehaviour {

    private List<GameObject> wheels = new List<GameObject>();

    void Start () {

        if (GameObject.FindGameObjectsWithTag("Wheel").Length == 0)
            Debug.LogError("Amount of lower tankwheels must be greater than 0");

        foreach (Transform wheel in gameObject.transform)
        {
            if (wheel.CompareTag("Wheel"))
            {
                wheels.Add(wheel.gameObject);
            }
        }

        wheels = wheels.OrderBy(w => w.transform.position.z).ToList();

        Debug.Log(wheels);

        GetComponent<LineRenderer>().positionCount = wheels.Count;

    }
	
	void Update () {
        foreach (GameObject wheel in wheels)
        {
            Vector3 connectionPoint = new Vector3(wheel.transform.position.x, wheel.transform.position.y - (0.5f / 2) + 0.04f, wheel.transform.position.z);
            GetComponent<LineRenderer>().SetPosition(wheels.IndexOf(wheel), connectionPoint);
            Debug.DrawLine(connectionPoint, connectionPoint + new Vector3(0.7f, 0, 0));
        }
    }
}
