using UnityEngine;
using UnityEngine.UI;
using Xennial.Services;
using Xennial.XR.Player;

public class SetSpawnPointButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private int _indexSpawn;

    private void Reset()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(SetSpawnPoint);    
    }

    public void SetSpawnPoint () 
    {
        ServiceLocator.Instance.Get<Player>().SpawnSettings.SetSpawnPoint(_indexSpawn);
    }
}
