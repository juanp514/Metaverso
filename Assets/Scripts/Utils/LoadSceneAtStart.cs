using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAtStart : MonoBehaviour
{
    [SerializeField]
    private int _indexScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(_indexScene);
    }
}
