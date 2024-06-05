using TMPro;
using UnityEngine;

namespace Xennial.XR.UI
{
    public class KeyboardViewBase : MonoBehaviour
    {
        [SerializeField]
        private Keyboard _keyboard;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _showAnimationKey = "IsShow";

        [SerializeField]
        private bool _visibleByDefault;

        private TMP_InputField _cachedInputField;

        public string Text
        {
            get
            {
                if (_cachedInputField != null)
                    return _cachedInputField.text;

                return string.Empty;
            }
            set
            {
                if (_cachedInputField != null)
                    _cachedInputField.text = value;
            }
        }

        public TMP_InputField InputField
        {
            get { return _cachedInputField;  }
        }

        protected virtual void Start()
        {
            _keyboard.OnInputReceived += SendKeyString;
            _keyboard.OnDone += OnDone;

            if(_visibleByDefault)
            {
                Show();
            }
        }

        protected void SetInputField(TMP_InputField inputField)
        {
            _cachedInputField = inputField;
        }

        public virtual void Show()
        {
            _keyboard.OnShow();
            _animator.SetBool(_showAnimationKey, true);
        }

        public virtual void Hide()
        {
            _animator.SetBool(_showAnimationKey, false);
        }

        #region Keyboard Receiving Input
        public void SendKeyString(string keyString)
        {
            if (_cachedInputField != null)
            {
                if (keyString.Length == 1 && keyString[0] == 8)
                {
#if UNITY_EDITOR
                    Debug.Log("KeyString: " + keyString);
#endif
                    if (_cachedInputField.text.Length > 0 && keyString.Equals("\x08"))
                    {
                        _cachedInputField.text = _cachedInputField.text.Remove(_cachedInputField.text.Length - 1);
                        Text = _cachedInputField.text;
                    }
                }
                else if(_cachedInputField.characterLimit == 0 || Text.Length < _cachedInputField.characterLimit)
                {
                    Text += keyString;
                }
            }
        }
        #endregion

        protected virtual void OnDone() { }
    }
}