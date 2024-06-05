using Xennial.Services;

namespace Xennial.XR.UI
{
    public class KeyboardView : KeyboardViewBase, IService
    {
        private void Awake()
        {
            Register();
        }

        private void OnDestroy()
        {
            Unregister();
        }

        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance.Unregister(this);
        }

        public void Show(XRInputField inputfield)
        {
            Show();
            SetInputField(inputfield);
        }

        public override void Hide()
        {
            SetInputField(null);
            base.Hide();
        }

        protected override void OnDone()
        {
            base.OnDone();

            if(InputField != null)
            {
                ((XRInputField)InputField).Deselect();
            }

            Hide();
        }
    }
}