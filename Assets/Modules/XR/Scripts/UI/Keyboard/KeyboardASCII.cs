using UnityEngine;

namespace Xennial.XR.UI
{
    public class KeyboardASCII : Keyboard
    {
        [SerializeField]
        public GameObject _alphaBoardUnshifted;

        [SerializeField]
        public GameObject _alphaBoardShifted;

        private bool _isShiftPressed = false;

        private void Awake()
        {
            Refresh();
        }

        public override void OnShow()
        {
            base.OnShow();
            ActiveBoardUnshifted();
        }

        public override void OnKeyDown(GameObject kb)
        {
            if (kb.name == "SHIFT")
            {
                _isShiftPressed = !_isShiftPressed;
                Refresh();
                return;
            }

            base.OnKeyDown(kb);
        }

        private void Refresh()
        {
            _alphaBoardUnshifted.SetActive(!_isShiftPressed);
            _alphaBoardShifted.SetActive(_isShiftPressed);
        }

        public void ActiveBoardUnshifted()
        {
            _isShiftPressed = false;
            Refresh();
        }

        public void ActiveBoardSfhifted()
        {
            _isShiftPressed = true;
            Refresh();
        }
    }
}