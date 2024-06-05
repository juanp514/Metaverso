using System.Collections;
using UnityEngine;
using Xennial.XR.Utils;

namespace Xennial.XR.Player
{
    public class FadeZone : FadeMesh
    {
        protected override void Awake()
        {
            base.Awake();
            _mesh.enabled = true;
        }

        private void Start()
        {
            StartCoroutine(SetMainCameraAsParent());
        }

        private IEnumerator SetMainCameraAsParent()
        {
            yield return new WaitUntil(()=> Camera.main != null);
            transform.parent = Camera.main.transform;
            transform.localPosition = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}