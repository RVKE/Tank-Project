using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables

    [Header("References")]
    public Transform playerTransform;

    public static GameManager instance;

    private PlayerInput input;

    #endregion

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
