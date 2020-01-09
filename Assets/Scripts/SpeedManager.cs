using UnityEngine;

public class SpeedManager : MonoBehaviour {

    public float leftTrackSpeed;
    public float rightTrackSpeed;

    void Update() {

    }

    public void PowerToRightTrack()
    {
        Debug.Log("RECHTS");
    }

    public void PowerToLeftTrack()
    {
        Debug.Log("LINKS");
    }
}
