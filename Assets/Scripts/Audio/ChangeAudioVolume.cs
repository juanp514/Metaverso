using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioVolume : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
    }

    public void ChangeVolume(float volume)
    {
            audioSource.volume =volume;
    }
}
