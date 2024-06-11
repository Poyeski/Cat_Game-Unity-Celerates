using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public string audioSourceObjectName = "AudioSource";
    public AudioSource audioSource;
    public AudioClip collectSound;
    public AudioClip missSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            FindAudioSource();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void FindAudioSource()
    {
        GameObject audioSourceObject = GameObject.Find(audioSourceObjectName);
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is missing on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("Specified AudioSource GameObject not found.");
        }
    }

    public void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    public void PlayMissSound()
    {
        if (audioSource != null && missSound != null)
        {
            audioSource.PlayOneShot(missSound);
        }
    }
}