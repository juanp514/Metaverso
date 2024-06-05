using UnityEngine;
using Xennial.Services;
using UnityEngine.XR.Interaction.Toolkit;

#if META_OPENXR_PRESENT
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR.Features.Meta;
#endif

namespace Xennial.XR.Player
{
    public class Player : MonoBehaviour, IService
    {
        [SerializeField]
        private FadeZone _fadeZone;

        [SerializeField]
        private TeleportationProvider _teleportationProvider;

        [SerializeField]
        private int _targetFrameRate = 90;

        [SerializeField]
        private XRRayInteractor[] _rayInteractors;

        [SerializeField]
        private XRRayInteractor[] _teleportInteractors;

        private SpawnSettings _spawnSettings = new SpawnSettings();

        public FadeZone FadeZone
        { 
            get => _fadeZone;
        }

        public TeleportationProvider TeleportationProvider 
        { 
            get => _teleportationProvider; 
        }

        public SpawnSettings SpawnSettings
        { 
            get => _spawnSettings;
        }

        private void Awake()
        {
            Register();
        }

        private void Start()
        {
#if META_OPENXR_PRESENT && !UNITY_EDITOR
            /*This code requires Meta Quest: Display Utilities and Meta Quest: AR Session to be active on XR Plug-in Managment.
              Framerate values available are 60, 72, 80, 90, 120*/
            XRDisplaySubsystem displaySubsystem = XRGeneralSettings.Instance
            .Manager
            .activeLoader
            .GetLoadedSubsystem<XRDisplaySubsystem>();

            bool success = displaySubsystem.TryRequestDisplayRefreshRate(_targetFrameRate);

            if (success)
            {
                Debug.Log("Target framerate request success.");
            }
            else
            {
                Debug.LogError("Target framerate request failed.");
            }
#endif
        }

        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance.Unregister(this);
        }

        private void OnDestroy()
        {
            Unregister();
        }

        public void SetUIRay(bool enabled)
        {
            for (int i = 0; i < _rayInteractors.Length; i++)
            {
                _rayInteractors[i].enabled = enabled;
            }
        }

        public void SetTeleport(bool enabled)
        {
            for (int i = 0; i < _teleportInteractors.Length; i++)
            {
                _teleportInteractors[i].enabled = enabled;
            }
        }
    }
}