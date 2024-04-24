using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.Play(); // Start playing the music

        // Prevent the GameObject from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
