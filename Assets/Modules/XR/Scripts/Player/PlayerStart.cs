#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Xennial.XR.Player
{
    public class PlayerStart : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField] 
        private float _spawnRadius;

        [Header("Use this property to select a Spawn Point: ServiceLocator.Instance.Get<Player>().SpawnSettings.SpawnPointIndex")]
        [SerializeField] 
        private Transform[] _spawnPoints;

        [SerializeField]
        private bool _doAutoFadeOut = true;

        [SerializeField]
        private bool _rayInteractorEnabled = true;

        [SerializeField]
        private bool _teleportEnabled = true;

        private static GameObject _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = Instantiate(_playerPrefab, transform.position, transform.rotation);                
                DontDestroyOnLoad(_instance);
            }

            Player player = _instance.GetComponentInChildren<Player>();

            if (player)
            {
                Transform target = _spawnPoints.Length > 0 && player.SpawnSettings.SpawnPointIndex >= 0 ? _spawnPoints[player.SpawnSettings.SpawnPointIndex] : transform;
                Vector3 position = target.position;

                if(_spawnRadius > 0)
                {
                    position += (Random.insideUnitSphere * _spawnRadius);
                    position.y = target.position.y;
                }

                TeleportRequest teleportRequest = new TeleportRequest()
                {
                    destinationPosition = position,
                    destinationRotation = target.rotation,
                    matchOrientation = MatchOrientation.TargetUpAndForward,
                    requestTime = 0
                };

                player.TeleportationProvider.QueueTeleportRequest(teleportRequest);

                if (_doAutoFadeOut)
                {
                    player.FadeZone.FadeOut();
                }

                player.SetUIRay(_rayInteractorEnabled);
                player.SetTeleport(_teleportEnabled);
                player.SpawnSettings.Reset();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(Selection.Contains(gameObject))
            {
                for (int i = 0; i < _spawnPoints.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(_spawnPoints[i].position, 0.1f);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(_spawnPoints[i].position, _spawnPoints[i].position + (_spawnPoints[i].forward * 0.25f));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(_spawnPoints[i].position, _spawnPoints[i].position + (_spawnPoints[i].right * 0.25f));
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(_spawnPoints[i].position, _spawnPoints[i].position + (_spawnPoints[i].up * 0.25f));
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(_spawnPoints[i].position, _spawnRadius);
                }

                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, _spawnRadius);
            }
        }
#endif
    }
}