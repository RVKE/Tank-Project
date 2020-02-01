using UnityEngine;

[System.Serializable]

public class Sound {

    #region Variables

    public AudioClip clip;

    public string name;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0f, 22000f)]
    public float lpCutoff;
    [Range(1f, 10f)]
    public float lpResonance;

    public bool loop = false;
    public bool enableLP = false;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public AudioLowPassFilter lpFilter;

    #endregion

}
