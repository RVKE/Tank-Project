using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Commanding,
    Driving,
}

public class GameManager : MonoBehaviour {


    public PlayerState currentState;

    public static GameManager instance;

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        currentState = PlayerState.Driving;
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
        if (currentState == PlayerState.Commanding)
            currentState = PlayerState.Driving;
        else
            currentState = PlayerState.Commanding;

        switch (currentState)
        {
            case PlayerState.Commanding:
                Debug.Log(currentState);
                break;
            case PlayerState.Driving:
                Debug.Log(currentState);
                break;
        }
    }
}
