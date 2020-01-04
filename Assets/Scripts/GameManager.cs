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

    [Header("Resources")]
    public int hitPointsAmount;
    public int energyAmount;
    public int dieselAmount;
    public int coalAmount;
    public int scrapMetalAmount;
    public int woodAmount;
    public int projectileAmount;

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
                break;
            case PlayerState.DRIVING:
                Debug.Log(currentState);
                break;
        }
    }
}
