using Sirenix.OdinInspector;
using UnityEngine;
using Xennial.TaskManager;
/// <summary>
/// The function this class will perform in the Mocaad project consist into detect when user selects
/// a object with it's hands and with this, trigger the completeTask event to continue with the next
/// timelines.
/// </summary>
public class HummingbirdBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private WaitUntilTask _taskToValidate;
    [SerializeField]
    private bool _isOnTask;
    [SerializeField]
    private HummingbirdCutOff _cutOff;
    [Button]
    private void Reset()
    {
        _audioSource = GetComponent<AudioSource>();
        _cutOff = GetComponent<HummingbirdCutOff>();
    }
    private void Awake()
    {
        _cutOff.enabled = false;
        _taskToValidate.OnPreExecutionEventHandler += UpdateIsOnTask;
    }
    private void UpdateIsOnTask()
    {
        _audioSource.clip = audios[0];
        _audioSource.Play();
        _isOnTask = true;
    }

    private void PlaySound()
    {

         _audioSource.loop = false;
         _audioSource.clip = audios[1];
        _audioSource.Play();

        Invoke("RepeatAudio", 2.5f);
    }
    /// <summary>
    /// The OnHimmingbirdSelected() Method will be called when select is triggered on the
    /// Unity event wrapper inside the same gameObject which contains this class.
    /// </summary>
    [Button]
    public void OnHummingbirdSelected()
    {
        if (_isOnTask)
        {
            _taskToValidate.CompleteTask();
            PlaySound();
            _isOnTask = false;
            _cutOff.enabled = true;
            _cutOff.Path();
        }
    }

    public void RepeatAudio()
    {
        _audioSource.loop = true;
        _audioSource.clip = audios[0];
        _audioSource.Play();
    }
}
