using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Xennial.XR.UI
{
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        public event Action<bool> OnValueChange;

        [SerializeField]
        private Sprite _default;

        [SerializeField]
        private Sprite _selected;

        [SerializeField]
        private ToggleButtonGroup _group;

        [SerializeField]
        private bool _isOn;

        private Button _button;
        private Image _target;

        public bool IsOn 
        { 
            get => _isOn;
        }
        public Button Button 
        { 
            get => _button;
        }

        private void Awake()
        {
            AddListener(OnClick);
            _target = transform.GetChild(0).GetComponent<Image>();

            if(_group != null)
            {
                _group.RegisterButton(this);
            }

            if(_isOn)
            {
                TurnOn();
            }
        }

        public void AddListener(UnityAction call)
        {
            if(_button == null)
            {
                _button = GetComponent<Button>();
            }

            _button.onClick.AddListener(call);
        }

        public void RemoveAllListeners()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            _button.onClick.RemoveAllListeners();
        }

        public void SetGroup(ToggleButtonGroup group)
        {
            _group = group;
            _group.RegisterButton(this);
        }

        private void OnClick()
        {
            if(_group != null)
            {
                _group.OnClick(this);
            }

            if(!IsOn)
            {
                TurnOn();
            }
            else if(_group == null)
            {
                TurnOff();
            }else
            {
                TurnOff();
            }

        }

        public virtual void TurnOn()
        {
            _isOn = true;
            _target.sprite = _selected;
            OnValueChange?.Invoke(_isOn);
        }

        public virtual void TurnOff()
        {
            _isOn = false;
            _target.sprite = _default;
            OnValueChange?.Invoke(_isOn);
        }
    }
}