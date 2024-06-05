using System;
using UnityEngine;

namespace Xennial.XR.UI
{
    public class Keyboard : MonoBehaviour
    {
        public event Action<string> OnInputReceived;
        public event Action OnDone;

        public virtual void OnKeyDown(GameObject kb)
        {
            if (kb.name == "DONE")
            {
                OnDone?.Invoke();
            }
            else
            {
                string s;

                if (kb.name == "BACKSPACE")
                    s = "\x08";
                else
                    s = kb.name;

                OnInputReceived?.Invoke(s);
            }
        }

        public virtual void OnShow() { }
    }
}