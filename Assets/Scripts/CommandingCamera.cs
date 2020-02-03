using UnityEngine;

public class CommandingCamera : MonoBehaviour {

    #region Variables

    [Header("Input")]
    public PlayerInput input;

    [Header("Camera Properties")]
    public float cameraSmoothingSpeed;

    public Transform utilityCameraTransform;
    public Transform dataCameraTransform;

    private Transform wantedTransform;

    #endregion

    void Start()
    {
        wantedTransform = utilityCameraTransform;
    }

    void Update () {

        transform.rotation = Quaternion.Lerp(transform.rotation, wantedTransform.rotation, cameraSmoothingSpeed);
        transform.position = Vector3.Lerp(transform.position, wantedTransform.position, cameraSmoothingSpeed);

        if (input.dKeyInput)
        {
            wantedTransform = dataCameraTransform;
        } else if (input.aKeyInput)
        {
            wantedTransform = utilityCameraTransform;
        }
    }
}
