using Sirenix.OdinInspector;
using UnityEngine;
using Xennial.TaskManager;

public class TaskTriggerer : MonoBehaviour
{
    [SerializeField]
    private TaskManagerGuided _taskManager;
    [Button]
    private void Reset()
    {
        _taskManager = FindObjectOfType<TaskManagerGuided>();
    }
    private void Start()
    {
        _taskManager.StartAllTask();
    }
}
