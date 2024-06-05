using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using Xennial.Services;

namespace Xennial.XR.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    [AddComponentMenu("UI/XR Input Field", 11)]
    public class XRInputField : TMP_InputField
    {
        [SerializeField]
        private bool _autofocus = true;

        private bool _isSelected;

        protected override void Start()
        {
            base.Start();

            if (_autofocus)
            {
                if (_autofocus)
                {
                    Select();
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(_isSelected)
            {
                ServiceLocator.Instance.Get<KeyboardView>().Hide();
                Deselect();
            }
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            SetBehaviourOnSelection();
        }

        private void SetBehaviourOnSelection()
        {
            if (!_isSelected)
            {
                _isSelected = true;
                ServiceLocator.Instance.Get<KeyboardView>().Show(this);
                StartCoroutine(WaitForValidGameobjectToHideKeyboard());
            }
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);
            SetBehaviourOnSelection();
        }

        private IEnumerator WaitForValidGameobjectToHideKeyboard()
        {
            while (_isSelected)
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    XRInputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<XRInputField>();

                    if (!EventSystem.current.currentSelectedGameObject.TryGetComponent(out KeyboardButton _))
                    {
                        if (inputField == null)
                        {
                            ServiceLocator.Instance.Get<KeyboardView>().Hide();
                            Deselect();
                            break;
                        }
                        else if (inputField != this)
                        {
                            Deselect();
                            break;
                        }
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        public void Deselect()
        {
            _isSelected = false;
        }
    }
}