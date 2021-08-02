using UnityEngine.Audio;
using MyBox;
using UnityEngine;
using System;   

//Isso daqui é do brackeys grande abraço pra eles
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public Sound[] _sounds;

    private void Awake()
    {

        if(instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        foreach (var s in _sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void Play(string soundName )
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        if (s == null) return;

        if( s.RandomPitch )
        {
            float randomPitch = UnityEngine.Random.Range( s.RangedPitchMin, s.RangedPitchMax);
            s.Source.pitch = randomPitch;
        }

        s.Source.Play();
    }

    public void Stop(string soundName )
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        if (s == null) return;

        s.Source.Stop();
    }
}
