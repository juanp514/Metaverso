using Sirenix.OdinInspector;
using UnityEngine;
using Xennial.TaskManager;

public class BirdsTriggerer : MonoBehaviour
{
    [SerializeField]
    private OnlyAudioTask _taskToTrigger;
    [SerializeField]
    private float TimeToStart = 4f;
    [SerializeField]
    private GameObject _birds;
    [SerializeField]
    private GameObject _butterflies;
    private void Start()
    {
        if(_birds !=null)
            _birds.SetActive(false);

        _butterflies.SetActive(false);
        _taskToTrigger.OnPreExecutionEventHandler += TriggerBirds;
    }
    [Button]
    private void TriggerBirds()
    {
        Invoke("Active", TimeToStart);
        
    }

    void Active()
    {
        if (_birds != null)
            _birds.SetActive(true);

        _butterflies.SetActive(true);
        _taskToTrigger.CompleteTask();
    }
}
