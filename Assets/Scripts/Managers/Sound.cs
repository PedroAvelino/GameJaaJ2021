using UnityEngine;
using MyBox;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip clip;
    
    [HideInInspector]
    public AudioSource Source;

    [Range(0f, 5f)]
    public float Volume;

    [Range(.1f, 3f)]
    public float Pitch;

    public bool Loop;

    public bool RandomPitch;

    public float RangedPitchMin = .1f;

    public float RangedPitchMax = 3f;

}