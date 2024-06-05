using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStopper : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _timeline;
    private void Awake()
    {
        _timeline.Play();
    }
    [Button]
    private void Reset()
    {
        _timeline = FindObjectOfType<PlayableDirector>();
    }
    private void Update()
    {
        if (_timeline.time <= 0.25f)
            return;
        _timeline.Stop();
        enabled = false;
    }
}
