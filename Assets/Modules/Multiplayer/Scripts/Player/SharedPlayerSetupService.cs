using System;
using UnityEngine;
using Xennial.Services;

namespace Xennial.Multiplayer
{
    public class SharedPlayerSetupService : MonoBehaviour, IService
    {
        public event Action OnHandTrackingModeChanged;

        [SerializeField]
        private Transform _head;

        [SerializeField]
        private Transform _leftHand;

        [SerializeField]
        private Transform _rightHand;

        [SerializeField]
        private Transform _leftController;

        [SerializeField]
        private Transform _rightController;

        private bool _isHandTrackingActive;

        public Transform Head 
        {
            get => _head;
        }
        public Transform LeftHand 
        { 
            get => _isHandTrackingActive ? _leftHand : _leftController;
        }
        public Transform RightHand 
        {
            get => _isHandTrackingActive ? _rightHand : _rightController;
        }

        public bool IsHandTrackingActive
        { 
            get => _isHandTrackingActive;
        }

        private void Start()
        {
            Register();
        }

        private void OnDestroy()
        {
            Unregister();
        }

        public void SetHandTracking(bool active)
        {
            _isHandTrackingActive = active;
            OnHandTrackingModeChanged?.Invoke();
        }

        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance.Unregister(this);
        }
    }
}