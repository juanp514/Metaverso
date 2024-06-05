using UnityEngine;

namespace Xennial.XR.UI
{
    public class XRCanvas : MonoBehaviour
    {
        private const float DISTANCE_MIN_THRESHOLD = 0.1f;

        [SerializeField]
        private bool _isLookAtEnable;

        [SerializeField]
        private bool _isFollowingEnable;

        [SerializeField]
        private float _followingDistance = 3;

        [SerializeField]
        private float _followingSpeed = 1;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            if(_isLookAtEnable)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        private void Update()
        {
            if(_isLookAtEnable)
            {
                transform.LookAt(_camera.transform);
            }

            if(_isFollowingEnable)
            {
                Vector3 targetPosition = _camera.transform.position + (_camera.transform.forward * _followingDistance);
                float playerDistance = Vector3.Distance(transform.position, _camera.transform.position);
                float targetDistance = Vector3.Distance(transform.position, targetPosition);

                if (playerDistance > _followingDistance || targetDistance < DISTANCE_MIN_THRESHOLD)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * (_followingSpeed * targetDistance));
                }
                else
                {
                    int dot = Vector3.Dot(_camera.transform.right, transform.forward) > 0 ? 1 : -1;
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.right * -dot), Time.deltaTime * (_followingSpeed * targetDistance));
                }
            }
        }
    }
}