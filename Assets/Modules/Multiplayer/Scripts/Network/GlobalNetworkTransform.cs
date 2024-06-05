using UnityEngine;

namespace Xennial.Multiplayer
{
    public class GlobalNetworkTransform : MonoBehaviour
    {
        private Transform _parent;

        public void SetParent(Transform parent)
        {
            _parent = parent;
            transform.SetParent(null);
        }

        private void Update()
        {
            transform.SetPositionAndRotation(_parent.position, _parent.rotation);
        }
    }
}