using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource sfxSource; // Audio source for playing sound effects
    public AudioSource musicSource; // Audio source for playing persistent music
    public static AudioController audioController = null; // Allows other scripts to call functions within AudioController

    Dictionary<string, AudioClip> soundEffects;

    // Awake is called when the script is loaded to the scene
    void Awake()
    {
        // If there is no instance of audioController, make it this
        if (audioController == null)
            audioController = this;
        // Enforces a singleton on the audioController ensuring there will only be one audioController
        else if (audioController != this)
            Destroy(gameObject);

        soundEffects = Load();

        // Sets AudioController to DontDestroyOnLoad so that it won't be destroyed when reloading a scene
        DontDestroyOnLoad(gameObject);
    }

    private Dictionary<string, AudioClip> Load()
    {
        Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();

        foreach (AudioClip ac in Resources.LoadAll<AudioClip>("Sounds/SFX/"))
        {
            sfx.Add(ac.name, ac);
            Debug.Log("Added " + ac.name + " to sound effects list!");
        }

        return sfx;
    }
    public string GetInteractSoundClip(GameObject go)
    {
        string modifiedName = go.name;
        if (modifiedName.Contains("("))
        {
            modifiedName = modifiedName.Remove(go.name.IndexOf('('));
        }
        Debug.Log("Modified Name " + modifiedName);
        return modifiedName;
    }

    // Function to play a single sound clip
    public void Play(string clipName)
    {
        // Sets the clip played by the sfx audio source to the input clip
        sfxSource.PlayOneShot(soundEffects[clipName]);
    }
    public void Pause()
    {
        sfxSource.Pause();
    }
    public void UnPause()
    {
        sfxSource.UnPause();
    }
    public void Stop()
    {
        sfxSource.Stop();
    }
}