using UnityEngine;

public class SpeedManager : MonoBehaviour {

    public float maxSpeed;
    public float acceleration;

    public float leftTrackCurrentSpeed;
    public float rightTrackCurrentSpeed;

    void Update() {

    }

    public void PowerToRightTrack(bool power)
    {
        Debug.Log("RECHTS: " + power);
        if(power)
        {
            rightTrackCurrentSpeed = Mathf.Round(Mathf.Lerp(rightTrackCurrentSpeed, maxSpeed, acceleration) * 100.0f) / 100.0f;
        } else
        {
            rightTrackCurrentSpeed = Mathf.Round(Mathf.Lerp(rightTrackCurrentSpeed, 0, acceleration) * 100.0f) / 100.0f;
        }
    }

    public void PowerToLeftTrack(bool power)
    {
        Debug.Log("RECHTS: " + power);
        if (power)
        {
            leftTrackCurrentSpeed = Mathf.Round(Mathf.Lerp(leftTrackCurrentSpeed, maxSpeed, acceleration) * 100.0f) / 100.0f;
        }
        else
        {
            leftTrackCurrentSpeed = Mathf.Round(Mathf.Lerp(leftTrackCurrentSpeed, 0, acceleration) * 100.0f) / 100.0f;
        }
    }
}
