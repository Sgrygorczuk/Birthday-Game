using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public static AudioManager Instance;
    [System.Serializable] public struct SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

    public AudioSource audioSource;
    public List<SoundEffect> soundLibrary;

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        // Populate dictionary for fast lookup
        foreach (var sound in soundLibrary)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary.Add(sound.name, sound.clip);
            }
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
        }
    }
}
