
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonDebugClick : MonoBehaviour
{
    [SerializeField]
    public Button _button;

    private void Reset()
    {
        _button = GetComponent<Button>();
    }

    [Button]
    private void Click()
    {
        _button.onClick.Invoke();
    }
}
