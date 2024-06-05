using System;
using System.Collections;
using UnityEngine;

namespace Xennial.XR.Utils
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FadeMesh : MonoBehaviour
    {
        protected MeshRenderer _mesh;
        private Material _meshMaterial;

        protected virtual void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterial = _mesh.material;
        }

        public void FadeIn()
        {
            FadeIn(null);
        }

        public void FadeIn(float time)
        {
            FadeIn(null, time);
        }

        public void FadeIn(Action onFadeInFinished, float time = 1)
        {
            StopAllCoroutines();
            Color targetColor = _meshMaterial.color;
            targetColor.a = 1;
            StartCoroutine(Fade(onFadeInFinished, time, targetColor));
        }

        public void FadeOut()
        {
            FadeOut(null);
        }

        public void FadeOut(float time)
        {
            FadeOut(null, time);
        }

        public void FadeOut(Action onFadeOutFinished, float time = 1)
        {
            StopAllCoroutines();
            Color targetColor = _meshMaterial.color;
            targetColor.a = 0;
            StartCoroutine(Fade(onFadeOutFinished, time, targetColor));
        }

        private IEnumerator Fade(Action onFadeFinished, float time, Color targetColor)
        {
            float timer = 0f;
            Color currentColor = _meshMaterial.color;

            while (timer < time)
            {
                _meshMaterial.SetColor("_BaseColor", Color.Lerp(currentColor, targetColor, timer / time));
                timer += Time.deltaTime;
                yield return null;
            }

            _meshMaterial.SetColor("_BaseColor", targetColor);
            onFadeFinished?.Invoke();
        }

        public void SetColor(Color color)
        {
            color.a = _meshMaterial.color.a;
            _meshMaterial.color = color;
        }

#if UNITY_EDITOR
        [ContextMenu("FadeIn")]
        public void FadeInEditor()
        {
            FadeIn();
        }

        [ContextMenu("FadeOut")]
        public void FadeOutEditor()
        {
            FadeOut();
        }
#endif
    }
}