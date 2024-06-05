using UnityEngine;

namespace Xennial.XR.Utils
{
    public class PersistentGameObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}