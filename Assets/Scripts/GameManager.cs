using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    COMMANDING,
    DRIVING,
}

public class GameManager : MonoBehaviour {


    public PlayerState currentState;

    public static GameManager instance;

    //private GameObject drivingCam;
    //private GameObject commandingCam;

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        currentState = PlayerState.DRIVING;
    }

    void Start()
    {
        //drivingCam = Camera.main.gameObject;
        //commandingCam = GameObject.FindGameObjectWithTag("Commanding Camera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpdatePlayerState();
        }
    }

    void UpdatePlayerState()
    {
        if (currentState == PlayerState.COMMANDING)
            currentState = PlayerState.DRIVING;
        else
            currentState = PlayerState.COMMANDING;

        switch (currentState)
        {
            case PlayerState.COMMANDING:
                Debug.Log(currentState);
                //commandingCam.SetActive(!commandingCam.activeInHierarchy);
                //drivingCam.SetActive(!commandingCam.activeInHierarchy);
                break;
            case PlayerState.DRIVING:
                Debug.Log(currentState);
                //commandingCam.SetActive(!commandingCam.activeInHierarchy);
                //drivingCam.SetActive(!commandingCam.activeInHierarchy);
                break;
        }
    }
}
