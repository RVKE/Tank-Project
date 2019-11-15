using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject dirLight;

    public int days;
    public int hours;


    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
    }
	void Start ()
    {
		
	}
	
	void Update ()
    {
        DayCycle();
    }

    void DayCycle ()
    {
        
    }
}
