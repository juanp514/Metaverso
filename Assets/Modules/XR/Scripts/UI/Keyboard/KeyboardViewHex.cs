using TMPro;
using UnityEngine;

namespace Xennial.XR.UI
{
    public class KeyboardViewHex : KeyboardViewBase
    {
        [SerializeField]
        private TMP_InputField _inputField;

        protected override void Start()
        {
            base.Start();
            SetInputField(_inputField);
        }
    }
}