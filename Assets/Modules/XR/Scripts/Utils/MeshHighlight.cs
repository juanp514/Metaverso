using System.Collections;
using UnityEngine;

namespace Xennial.XR.Utils
{
    public class MeshHighlight : MonoBehaviour
    {
        protected const string EMISSION_COLOR_NAME = "_EmissionColor";
        protected const string EMISSION_INTENSITY_NAME = "_EmissionActive";

        [SerializeField]
        private Color _color = Color.cyan;

        [SerializeField]
        private int _defaultMaterialIndex = 0;

        [SerializeField]
        private AnimationCurve _animation = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 0.5f), new Keyframe(2, 0f) });

        protected MeshRenderer _renderer;
        protected Material _currentMaterial;
        private Coroutine _animationCoroutine;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _currentMaterial = _renderer.materials[_defaultMaterialIndex];
        }

        public void Highlight()
        {
            _currentMaterial.SetColor(EMISSION_COLOR_NAME, _color);
            _animationCoroutine = StartCoroutine(Animate());
        }

        public virtual void Stop()
        {
            if(_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
                _currentMaterial.SetFloat(EMISSION_INTENSITY_NAME, 0);
            }
        }

        private IEnumerator Animate()
        {
            float maxTime = _animation.keys[_animation.length - 1].time;
            float time = 0;

            while (true)
            {
                time += Time.deltaTime;

                if (time > maxTime)
                {
                    time = time - maxTime;
                }

                _currentMaterial.SetFloat(EMISSION_INTENSITY_NAME, _animation.Evaluate(time));
                yield return new WaitForEndOfFrame();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Test Animation")]
        private void TestAnimate()
        {
            if (Application.isPlaying)
            {
                _currentMaterial = _renderer.materials[0];
                _currentMaterial.SetColor(EMISSION_COLOR_NAME, _color);
                _animationCoroutine = StartCoroutine(Animate());
            }
            else
            {
                Debug.LogWarning("This animation will be triggered on play mode!");
            }
        }

        [ContextMenu("Test Stop")]
        private void TestStop()
        {
            Stop();
        }
#endif
    }
}