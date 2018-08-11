using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour 
{
    public static SoundManager instance;
    
    [Tooltip("Minimum number of Audio Sources to keep alive")]
    [Range(1,5)] public int minSources = 1;

    [Tooltip("Countdown after a played clip to execute a clean up of sources")]
    [Range(5,10)] public int cleanUpInterval = 5;

    [Range(0,1), SerializeField] private float musicVolume = 0.75f;
    [Range(0,1), SerializeField] private float sfxVolume = 0.75f;

    public event Action OnMuiscVolumeChange = delegate { };
    
    bool hasScheduledCleanup;
    List<AudioSource> audioSources = new List<AudioSource>();

    public float SfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = MustBeBetween(value); }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = MustBeBetween(value); OnMuiscVolumeChange();}
    }

    void Awake ()
    {
        // Create as a singleton
        instance = this;
        // Populate the initial List<> of Audio Source Components
        BuildAudioSourcesList();
        // Get Volume
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SfxVolume = PlayerPrefs.GetFloat("SFXVolume");
    }

    private float MustBeBetween(float value, float min = 0f, float max = 1f)
    {
        return value < min ? min : (value > max ? max : value);
    }
    
    /// Return first Audio Source that is not playing an audio clip
    public AudioSource GetOpenAudioSource ()
    {        
        int _audioSourcesTried = 0;

        if (audioSources.Count > 0)
        {
            if (!hasScheduledCleanup)
            {
                StartCoroutine(CleanUpAudioSources());
                hasScheduledCleanup = true;
            }

            foreach (AudioSource _audioSource in audioSources.ToArray())
            {
                _audioSourcesTried++;
                
                if (!_audioSource.isPlaying)
                {
                    return _audioSource;
                }

                if (_audioSourcesTried >= audioSources.Count)
                {
                    AudioSource newAudioSource = this.gameObject.AddComponent<AudioSource>();
                    newAudioSource.playOnAwake = false;
                    audioSources.Add(newAudioSource);
                    return newAudioSource;
                }
            }
        }
        // If we get here, we got a problem.
        Debug.LogError("Unable to acces or create AudioSource component");
        return null;
    }

    /// Builds the initial List<> of Audio Source Componenets in the GameObject
    void BuildAudioSourcesList ()
    {
        foreach (AudioSource _audioSource in GetComponents<AudioSource>())
        {
            audioSources.Add(_audioSource);
        }
    }

    /// Cleans the List<> of Audio Source Componenets in the GameObject
    IEnumerator CleanUpAudioSources ()
    {
        yield return new WaitForSeconds(cleanUpInterval);
        
        int _audioSourcesToTry = minSources;
        
        foreach (AudioSource _audioSource in audioSources.ToArray())
        {
            if (!_audioSource.isPlaying && _audioSourcesToTry < audioSources.Count)
            {
                audioSources.Remove(_audioSource);
                Destroy(_audioSource);
            }
            _audioSourcesToTry++;
        }

        hasScheduledCleanup = false;
    }
}
