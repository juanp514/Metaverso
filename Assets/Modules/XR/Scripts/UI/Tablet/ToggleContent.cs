using UnityEngine;
using Xennial.XR.UI;

namespace Xennial.XR.UI
{
    public class ToggleContent : MonoBehaviour
    {
        [SerializeField]
        private ToggleButton _toggleButton;

        private void Awake()
        {
            _toggleButton.OnValueChange += OnValueChange;
            OnValueChange(_toggleButton.IsOn);
        }

        private void OnValueChange(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}