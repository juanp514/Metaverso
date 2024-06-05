using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Xennial.XR.UI
{
    public class TabletBase : MonoBehaviour
    {
        private const string PRIMARY_BUTTON_NAME = "PrimaryButton";
        private const string RIGHT_CLICK_NAME = "RightClick";
        private const string XR_UI_LEFT_INTERACTION_NAME = "XRI LeftHand Interaction";
        private const string XR_UI_RIGHT_INTERACTION_NAME = "XRI RightHand Interaction";
        private const string XRI_UI_NAME = "XRI UI";

        [SerializeField]
        private LocomotionProvider _locomotionProvider;
        [SerializeField]
        private GameObject _tabletContent;

        [Space(50)]
        [Header("Controls")]
        [SerializeField]
        private Transform _leftController;
        [SerializeField]
        private Transform _rightController;

        private const float DISTANCE_FROM_CONTROLLER = 0.225F;

        public Action<bool> OnTabletStatus;

        private bool _isGrabbed = false;
        private InputAction _leftControllerOpenAction;
        private InputAction _rightControllerOpenAction;
        private InputAction _altOpenAction;

        private InputActionManager _inputActionManager;

        public GameObject TabletContent { get => _tabletContent; }
        public Transform LeftController { get => _leftController; }
        public Transform RightController { get => _rightController; }

        private void Awake()
        {
            if (_leftController == null || _rightController == null)
            {
                Debug.LogError("You must Set the controllers in inspector");
                return;
            }

            _inputActionManager = FindAnyObjectByType<InputActionManager>();

            InputActionMap inputActionAsset = _inputActionManager.actionAssets[0].actionMaps.ToList().Find(a => a.name == XR_UI_LEFT_INTERACTION_NAME);
            _leftControllerOpenAction = inputActionAsset.FindAction(PRIMARY_BUTTON_NAME);

            inputActionAsset = _inputActionManager.actionAssets[0].actionMaps.ToList().Find(a => a.name == XR_UI_RIGHT_INTERACTION_NAME);
            _rightControllerOpenAction = inputActionAsset.FindAction(PRIMARY_BUTTON_NAME);

            inputActionAsset = _inputActionManager.actionAssets[0].actionMaps.ToList().Find(a => a.name == XRI_UI_NAME);
            _altOpenAction = inputActionAsset.FindAction(RIGHT_CLICK_NAME);

            _leftControllerOpenAction.performed += OpenLeftController;
            _rightControllerOpenAction.performed += OpenRightController;
            _altOpenAction.performed += AltAction;
            _tabletContent.SetActive(false);

            _locomotionProvider.beginLocomotion += OnLocomotion;
        }

        private void OnLocomotion(LocomotionSystem system)
        {
            if (!_isGrabbed)
            {
                DeactivateTablet();
            }
        }

        private void OnDestroy()
        {
            _leftControllerOpenAction.performed -= OpenLeftController;
            _rightControllerOpenAction.performed -= OpenRightController;
            _altOpenAction.performed -= AltAction;
        }

        private void AltAction(InputAction.CallbackContext obj)
        {
            transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 0.4f);
            Vector3 offset = transform.position - Camera.main.transform.position;
            transform.LookAt(transform.position + offset);
            _tabletContent.SetActive(true);
        }

        private void TabletStatusManager(Transform controller, bool changeStatus = true)
        {
            if (!_tabletContent.activeSelf)
            {
                transform.position = controller.position + (Vector3.up * DISTANCE_FROM_CONTROLLER);
                Vector3 offset = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + offset);
            }

            if(changeStatus)
            {
                _tabletContent.SetActive(!_tabletContent.activeSelf);
                OnTabletStatus?.Invoke(_tabletContent.activeSelf);
            }
            
        }

        private void OpenLeftController(InputAction.CallbackContext context)
        {
            TabletStatusManager(_leftController);
        }

        private void OpenRightController(InputAction.CallbackContext context)
        {
            TabletStatusManager(_rightController);
        }

        public void OpenTabletByHand(Transform handTransform, bool changeStatus)
        {
            TabletStatusManager(handTransform, changeStatus);
        }

        public void DeactivateTablet()
        {
            _tabletContent.SetActive(false);
            OnTabletStatus?.Invoke(false);
        }

        public void OnBoolGrabbed(bool grabbed)
        {
            _isGrabbed = grabbed;
        }
    }
}