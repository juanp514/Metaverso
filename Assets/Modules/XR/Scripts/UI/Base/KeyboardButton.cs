using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Xennial.XR.UI
{
    [RequireComponent(typeof(KeyboardButtonData))]
    public class KeyboardButton : Button
    {
        private KeyboardButtonData _data;
        private bool _isHover;

        protected override void Start()
        {
            base.Start();
            _data = GetComponent<KeyboardButtonData>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _data.Hover();
            _isHover = true;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _data.Unhover();
            _isHover = false;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _data.Press();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (_isHover)
                _data.Hover();
            else
                _data.Unhover();
        }
    }
}