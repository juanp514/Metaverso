using ReadyPlayerMe.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xennial.Services;

namespace Xennial.Multiplayer
{
    public class AvatarSetup : MonoBehaviour
    {
        public event Action<bool> OnAvatarVisibilityChanged;
        public event Action OnInitialized;

        [SerializeField]
        private GameObject _defaultAvatar;

        [SerializeField]
        private SharedPlayer _player;

        [SerializeField]
        private Transform _head, _leftHand, _rightHand;

        private List<GameObject> _avatarsOtherComponents = new List<GameObject>();
        private Transform _avatarNeck;
        private Transform _avatarLeftHand;
        private Transform _avatarRightHand;
        private Transform _armature;

        private int _currentAvatar;
        private SharedPlayerSetupService _playerSetupService;

        public int CurrentAvatar 
        { 
            get => _currentAvatar; 
        }

        private void Awake()
        {
            StartCoroutine(WaitForSpawnSharedPlayer());
        }

        private IEnumerator WaitForSpawnSharedPlayer()
        {
            yield return new WaitUntil(() => _player.IsSpawned);

            _playerSetupService = ServiceLocator.Instance.Get<SharedPlayerSetupService>();

            if (!_player.HasStateAuthority)
            {
                LoadAvatar();
            }
            else
            {
                SharedPlayerSetupService setup = _playerSetupService;
                _head.gameObject.AddComponent<GlobalNetworkTransform>().SetParent(setup.Head);
                _leftHand.gameObject.AddComponent<GlobalNetworkTransform>();
                _rightHand.gameObject.AddComponent<GlobalNetworkTransform>();
                SetHandParents();
                setup.OnHandTrackingModeChanged += OnHandTrackingModeChanged;
            }
        }

        public void LoadAvatar()
        {
            StartProcessToLoadAvatar();
        }

        public void StartProcessToLoadAvatar()
        {
            AvatarObjectLoader avatarLoader = new AvatarObjectLoader();
            avatarLoader.OnCompleted += OnAvatarLoaded;
            avatarLoader.OnFailed += OnAvatarFailed;

            if (!string.IsNullOrEmpty(_player.AvatarUrl))
            {
                avatarLoader.LoadAvatar(_player.AvatarUrl);
            }
            else
            {
                LoadDefaultAvatar();
            }
        }

        private void OnHandTrackingModeChanged()
        {
            SetHandParents();
            SetupAvatarHandsPositionAndRotation(_playerSetupService.IsHandTrackingActive);
        }

        private void SetHandParents()
        {
            SharedPlayerSetupService setup = _playerSetupService;
            _leftHand.GetComponent<GlobalNetworkTransform>().SetParent(setup.LeftHand);
            _rightHand.GetComponent<GlobalNetworkTransform>().SetParent(setup.RightHand);
        }

        private void LoadDefaultAvatar()
        {
            _defaultAvatar.SetActive(true);
            _defaultAvatar.transform.parent = transform;
            SetupBodyParts(_defaultAvatar.transform, true);

            _armature.transform.parent = _head;
            _armature.localPosition = Vector3.zero;
            _armature.localScale = new Vector3(1, 0, 1);
            _avatarNeck.localEulerAngles = new Vector3(0, 180, 0);
        }

        private void OnAvatarFailed(object sender, FailureEventArgs args)
        {
#if UNITY_EDITOR
            Debug.LogError($"{args.Type} - {args.Message}");
#endif
            LoadDefaultAvatar();
        }

        private void OnAvatarLoaded(object sender, CompletionEventArgs args)
        {
            GameObject avatar = args.Avatar;
            if (args.Metadata.BodyType == BodyType.FullBody)
            {
                LoadDefaultAvatar();
                Destroy(avatar);
                return;
            }

            avatar.transform.parent = transform;
            SetupBodyParts(avatar.transform, true);
            OnInitialized?.Invoke();

            _armature.transform.parent = _head;
            _armature.localPosition = Vector3.zero;
            _armature.localScale = new Vector3(1, 0, 1);
        }

        private void SetupBodyParts(Transform parentToShow, bool isParent)
        {
            foreach (Transform child in parentToShow)
            {
                SetBodyParts(child.gameObject);

                if (child.childCount > 0)
                {
                    SetupBodyParts(child, false);
                }
            }

            if (isParent)
            {
                SetupPartsInAvatarParent();
            }
        }

        private void SetupPartsInAvatarParent()
        {
            _avatarLeftHand.parent = _leftHand;
            _avatarRightHand.parent = _rightHand;
            SharedPlayerSetupService setup = _playerSetupService;
            SetupAvatarHandsPositionAndRotation(setup.IsHandTrackingActive);
            _avatarNeck.parent = _head;
            _avatarNeck.localPosition = Vector3.zero;
            _avatarNeck.localRotation = Quaternion.identity;
        }

        public void SetupAvatarHandsPositionAndRotation(bool isUsingHandTracking)
        {
            if (isUsingHandTracking)
            {
                _avatarLeftHand.localPosition = Vector3.zero;
                _avatarRightHand.localPosition = Vector3.zero;
            }
            else
            {
                _avatarLeftHand.localPosition =  new Vector3(-0.05f, 0, -0.11f);
                _avatarRightHand.localPosition = new Vector3(0.05f, 0, -0.11f);
            }

            _avatarLeftHand.localEulerAngles = new Vector3(90, 0, 0);
            _avatarRightHand.localEulerAngles = new Vector3(90, 0, 0);
        }

        [ContextMenu("GetEulerAngles")]
        private void GetEulerAngles()
        {
            Debug.Log($"L {_leftHand.parent.localEulerAngles} \n R{_rightHand.parent.localEulerAngles}");
        }

        private void SetBodyParts(GameObject part)
        {
            if (part.name.Contains("Armature") || part.name.Contains("Hips"))
            {
                _armature = part.transform;
            }

            if ((part.name.Contains("Wolf3D") && !part.name.Contains("Hands")) || part.name.Contains("Hands") || (part.name.Contains("Renderer_Avatar")))
            {
                _avatarsOtherComponents.Add(part);
            }

            switch (part.name)
            {
                case "LeftHand":
                    _avatarLeftHand = part.transform;
                    break;
                case "RightHand":
                    _avatarRightHand = part.transform;
                    break;
                case "Neck":
                    _avatarNeck = part.transform;
                    break;
            }

            SetAvatarVisibility(_player.IsAvatarVisible);
        }

        public void SetAvatarVisibility(bool isVisible)
        {
            foreach (var ob in _avatarsOtherComponents)
            {
                ob.GetComponent<SkinnedMeshRenderer>().enabled = isVisible;
            }

            OnAvatarVisibilityChanged?.Invoke(isVisible);
        }
    }
}