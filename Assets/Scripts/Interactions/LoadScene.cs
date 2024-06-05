
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xennial.Services;
using Xennial.XR.Player;

public class LoadScene : MonoBehaviour
{
    [Button]
    private void Test() 
    {
        ChangeSceneByIndex(5);
    }
    public void ChangeSceneByIndex(int _sceneBuildIndex)
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ServiceLocator.Instance.Get<Player>().FadeZone.FadeIn(() => SceneManager.LoadScene(_sceneBuildIndex));
    }
}
