using Fusion;
using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xennial.API;
using Xennial.Services;

namespace Xennial.Multiplayer
{
    public class SessionCreator : MonoBehaviour
    {
        [SerializeField]
        private bool _autoCreateSessionOnStart = false; 

        private NetworkRunner _runner;

        private void Start()
        {
            ServiceLocator.Instance.Get<SessionService>().OnSessionStartRequested += OnSessionStartRequested;

            if (_autoCreateSessionOnStart) 
            {
                OnSessionStartRequested("TestRoom");
            }
        }

        public void OnSessionStartRequested(string sessionCode)
        {
            AvatarCache.Clear();
            GameMode mode = GameMode.Shared;

            _runner = gameObject.GetComponent<NetworkRunner>();
            _runner.ProvideInput = false;

            string path = "Assets/Content/Exterior/Scenes/Exterior.unity";

            SceneRef scene = SceneRef.FromPath(path);
            NetworkSceneInfo sceneInfo = new();

            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionCode,
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerCustom>().SetAddressableScenePath(path)
            });
        }
    }
}