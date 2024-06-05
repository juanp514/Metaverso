using UnityEngine;

namespace Xennial.Multiplayer
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private bool _lookAtEnabled = true;

        private Transform _camera;

        private void Start()
        {
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            if (!_target)
            {
                return;
            }

            transform.position = _target.position + _offset;

            if (_lookAtEnabled)
            {
                transform.LookAt(_camera);
            }
        }
    }
}