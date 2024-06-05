using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private GameObject audioIcon;

    private bool isPlaying = false;
    private float volume = 0;

    [SerializeField]
    private UnityEvent _onAudioEnd;
    [SerializeField]
    private UnityEvent _onAudioStop;

    private void Start()
    {
        if (audioIcon != null)
            audioIcon.SetActive(false);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        audio.Play();
        StartCoroutine(CallAfterTime(audio.clip.length));
        volume = 0;
        if (audioIcon != null)
            audioIcon.SetActive(true);

        CancelInvoke();
        isPlaying = true;
    }

    private IEnumerator CallAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        OnAudioEnd();
    }

    private void OnAudioEnd()
    {
        _onAudioEnd.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        isPlaying = false;
        Invoke("StopAudio", 2f);
        StopAllCoroutines();
    }


    private void ChangeVolume(int dir)
    {
        volume += dir * 0.4f * Time.deltaTime;
        volume = Mathf.Clamp(volume, 0, 0.7f);
        audio.volume = volume;
    }

    private void StopAudio()
    {
        if (audioIcon != null)
            audioIcon.SetActive(false);
        audio.Stop();
    }

}
