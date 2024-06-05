using System.Collections;
using UnityEngine;
using Xennial.TaskManager;

public class AnimationSpeedController : MonoBehaviour
{
    [SerializeField]
    private WaitUntilTask taskToActivate;
    [SerializeField]
    private AudioSource[] _AudioSource;
    [SerializeField]
    private float TimeToStart;
    [SerializeField]
    private float TimeToStartMusic = 1f;
    [SerializeField]
    private Animator animator; 
    [SerializeField]
    private float animationSpeed = 1.0f;
    [SerializeField]
    private float maxVolume1 = 0.6f;
    [SerializeField]
    private float maxVolume2 = 0.4f;
    [SerializeField]
    private float fadeDuration = 3.0f;

    void Start()
    {
        taskToActivate.OnPreExecutionEventHandler += ActiveAnimator;
        
    }

    protected virtual void ActiveAnimator()
    {

        Invoke("Active", TimeToStart);
    }

    void Active()
    {
        animator.enabled = true;

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Obtener la referencia del Animator
        }

        animator.speed = animationSpeed; // Establecer la velocidad de la animación
        Invoke("MetalBoxSound", 0.3f);
        Invoke("BackgroundSound", TimeToStartMusic);
    }

    void MetalBoxSound()
    {
        _AudioSource[0].Play();
    }

    void BackgroundSound()
    {
        _AudioSource[1].Play();
        StartCoroutine(FadeInAudioSource(_AudioSource[1], fadeDuration, maxVolume1));
        _AudioSource[2].Play();
        StartCoroutine(FadeInAudioSource(_AudioSource[2], fadeDuration, maxVolume2));
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
    }
}
