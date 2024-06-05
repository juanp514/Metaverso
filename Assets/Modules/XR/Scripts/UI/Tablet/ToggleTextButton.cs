using TMPro;
using UnityEngine;

namespace Xennial.XR.UI
{
    public class ToggleTextButton : ToggleButton
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Color _textDefaultColor;

        [SerializeField]
        private Color _textSelectedColor;

        public string Text
        {
            get { return _text.text; }
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            _text.color = _textSelectedColor;
        }

        public override void TurnOff()
        {
            base.TurnOff();
            _text.color = _textDefaultColor;
        }
    }
}