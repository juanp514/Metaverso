using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class BlackZoneTriggerer : MonoBehaviour
{
    //[SerializeField]
    //private BlackZone _blackZone;
    [SerializeField]
    private PlayableDirector _timeline;
    [Button]
    private void Reset()
    {
        _timeline = FindObjectOfType<PlayableDirector>(false);
        //_blackZone = FindObjectOfType<BlackZone>();
    }
    private void Awake()
    {
        //_blackZone.DoFadeIn(null, 3);
    }
    [Button]
    private void OnDisable()
    {
        //_blackZone.gameObject.SetActive(true);
        //_blackZone.DoFadeOut(null, 3);
    }
}
