using UnityEngine;
using Xennial.TaskManager;

public class GenericTaskValidator : MonoBehaviour
{
    [SerializeField]
    private WaitUntilTask _taskToValidate;
    private void OnDisable()
    {
        _taskToValidate.CompleteTask();
    }
}
