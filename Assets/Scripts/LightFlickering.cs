using UnityEngine;


[RequireComponent(typeof(Light))]
public class LightFlickering : MonoBehaviour {

    #region Variables

    [Header("Light Flicker Settings")]
    public float flickeringDelay;
    public float flickerSmoothingSpeed;

    public float minLightIntensity;
    public float maxLightIntensity;

    private float lightIntensity;

    #endregion

    void Start()
    {
        InvokeRepeating("Flicker", 0.0f, flickeringDelay);
    }

    void Update()
    {
        Light lightSource = GetComponent<Light>();
        if (lightSource)
        {
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, lightIntensity, flickerSmoothingSpeed);
        }
    }

    void Flicker()
    {
        lightIntensity = Random.Range(minLightIntensity, maxLightIntensity);
    }

}
