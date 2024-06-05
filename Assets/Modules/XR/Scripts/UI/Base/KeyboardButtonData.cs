using UnityEngine;
using UnityEngine.UI;

namespace Xennial.XR.UI
{
    public class KeyboardButtonData : MonoBehaviour
    {
        [SerializeField]
        private Sprite _normalIcon;

        [SerializeField]
        private Sprite _hoverIcon;

        [SerializeField]
        private Sprite _pressIcon;

        [SerializeField]
        private Sprite _normalBackground;

        [SerializeField]
        private Sprite _pressBackground;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _pressedAnimationKey = "IsPressed";

        public void Unhover()
        {
            _background.sprite = _normalBackground;
            _icon.sprite = _normalIcon;
            _animator.SetBool(_pressedAnimationKey, false);
        }

        public void Hover()
        {
            _background.sprite = _normalBackground;
            _icon.sprite = _hoverIcon;
            _animator.SetBool(_pressedAnimationKey, false);
        }

        public void Press()
        {
            _background.sprite = _pressBackground;
            _icon.sprite = _pressIcon;
            _animator.SetBool(_pressedAnimationKey, true);
        }
    }
}