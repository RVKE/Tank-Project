using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    #region Variables

    [Header("Input")]
    public Camera cam;

    public Vector3 mouseWorldPosition;

    public float forwardInput;
    public float rotationInput;
    public bool tabKeyInput;
    public bool aKeyInput;
    public bool dKeyInput;

    #endregion

    void Update () {
        if (cam)
            HandleInput();
	}

    protected void HandleInput()
    {
        tabKeyInput = Input.GetKeyDown(KeyCode.Tab);
        aKeyInput = Input.GetKeyDown(KeyCode.A);
        dKeyInput = Input.GetKeyDown(KeyCode.D);
        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        Ray screenRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(screenRay, out hit))
        {
            mouseWorldPosition = hit.point;
        }
    }
}
