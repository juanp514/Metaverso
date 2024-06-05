using System.Collections.Generic;
using UnityEngine;

namespace Xennial.XR.UI
{
    public class ToggleButtonGroup : MonoBehaviour
    {
        private List<ToggleButton> _buttons = new List<ToggleButton>();

        public void RegisterButton(ToggleButton button)
        {
            _buttons.Add(button);
        }

        public void OnClick(ToggleButton button)
        {
            _buttons.RemoveAll(b => b == null);

            for (int i = 0; i < _buttons.Count; i++)
            {
                if(button != _buttons[i])
                {
                    _buttons[i].TurnOff();
                }
            }
        }
    }
}