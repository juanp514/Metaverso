using System.Collections;
using TMPro;
using UnityEngine;

namespace Xennial.Multiplayer
{
    public class UISharedPlayer : MonoBehaviour
    {
        [SerializeField]
        private SharedPlayer _player;

        [SerializeField]
        private TMP_Text _playerName;

        private void Awake()
        {
            StartCoroutine(WaitForSpawnSharedPlayer());
        }

        private IEnumerator WaitForSpawnSharedPlayer()
        {
            yield return new WaitUntil(() => _player.IsSpawned);
            _playerName.text = _player.PlayerName;
            gameObject.SetActive(!_player.HasStateAuthority);
        }
    }
}