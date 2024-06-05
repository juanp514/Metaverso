using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    [SerializeField] private RectTransform progresBar;
    [SerializeField] private float limit = -5f;

    private bool isPlaying=false;
    private float volume = 0;

    private void Start()
    {
        video.Play();
        video.SetDirectAudioVolume(0, 0);
        Invoke("StopVideo", 0.5f);
    }
    private void Update()
    {
        if (isPlaying)
        {
            ChangeVolume(1);
        }
        else
        {
            ChangeVolume(-1);
        }
        UpdateProgresBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        CallPlayVideo();
    }

    private void OnTriggerExit(Collider other)
    {
        StopVideo();
    }

    public void CallPlayVideo()
    {
        video.Play();
        volume = 0;
        CancelInvoke();
        isPlaying = true;
    }


    private void ChangeVolume(int dir)
    {
        volume += dir *0.4f * Time.deltaTime;
        volume = Mathf.Clamp(volume, 0, 0.7f);
        video.SetDirectAudioVolume(0, volume);
    }

    public void StopVideo()
    {
        isPlaying = false;
        video.Stop();
    }

    private void UpdateProgresBar()
    {
        Vector3 currentPos = progresBar.anchoredPosition;
        currentPos.x = (limit/(float)video.length)*(float)video.clockTime;
        progresBar.anchoredPosition = currentPos;
     
    }
}
