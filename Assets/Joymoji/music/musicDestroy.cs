using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class musicDestroy : MonoBehaviour
{
    public AudioClip otherClip;
    AudioSource audioSource;

    static bool AudioBegin = false;
    void Awake()
    {
        /*if (!AudioBegin)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }*/
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
    }
    
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = otherClip;
            audioSource.Play();
        }
    }
}
