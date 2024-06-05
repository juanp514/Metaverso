using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Xennial.TaskManager;

public class FinalFadeOut : MonoBehaviour
{
    [SerializeField]
    private Volume Fade;
    [SerializeField]
    private WaitUntilTask taskToActivate;
    [SerializeField]
    private AudioSource[] _AudioSource;
    [SerializeField]
    private float startTime;
    [SerializeField]
    public float duration = 0.5f;

    private void Start()
    {
        taskToActivate.OnPreExecutionEventHandler += ActivateTimerFadeEffect;
    }

    private void ActivateTimerFadeEffect()
    {
        Invoke("TimerFadeEffect", startTime);
    }

    private void TimerFadeEffect()
    {
        StartCoroutine(FadeEffect());
        foreach (var audioSource in _AudioSource)
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Comenzar la reproducción
                StartCoroutine(FadeInAudioSource(audioSource, duration, 0)); // Iniciar el aumento gradual del volumen
            }
        }
    }

    protected virtual IEnumerator FadeEffect()
    {
        float currentDuration = 0f;


        while (currentDuration < duration)
        {
            float t = currentDuration / duration;
                float fadeAmountIn = Mathf.Lerp(0f, 1f, t);
                Fade.weight = fadeAmountIn;

                currentDuration += Time.deltaTime;
                yield return null;
        }

        Fade.weight = 1f;

    }

    IEnumerator FadeInAudioSource(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
