using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using Xennial.TaskManager;

public class TimelineTriggerer : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _timelineObject;
    [SerializeField]
    private OnlyAudioTask _tastTotriggerTimeline;
    private void Start()
    {
        _tastTotriggerTimeline.OnPreExecutionEventHandler += TriggerTimeline;
    }
    [Button]
    private void Reset()
    {
        _timelineObject = FindObjectOfType<PlayableDirector>();
    }
    [Button]
    private void TriggerTimeline()
    {
        _timelineObject.Play();
    }
}
