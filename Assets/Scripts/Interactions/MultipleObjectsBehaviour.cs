using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectsBehaviour : ObjectsBehaviour
{
    [SerializeField]
    private List<GameObject> ParallelObjects;
    [SerializeField]
    private float startValueDissolveParallel = 0.78f;
    [SerializeField]
    private float endValueDissolveParallel = 0.339f;

    protected override void ActivateDissolveEffect()
    {
        StartCoroutine(ProcessChandeliersSequentially());
    }

    protected override IEnumerator ProcessChandeliersSequentially()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            // Activa y procesa el objeto secuencial
            GameObject sequentialObj = Objects[i];
            GameObject parallelObj = ParallelObjects[i];
            sequentialObj.SetActive(true);
            parallelObj.SetActive(true);
            if (audio != null)
            {
                for (int s = 0; s < Objects.Count; s++)
                {
                    _audioSource[s].PlayOneShot(audio);
                }
            }
            StartCoroutine(DissolveObjects(parallelObj, startValueDissolveParallel, endValueDissolveParallel));
            yield return StartCoroutine(DissolveObjects(sequentialObj, startValueDissolve, endValueDissolve));
        }

        if (CompleteTaskOnEnd)
        {
            Invoke("FinishTask", NextTask);
        }
    }

    protected IEnumerator DissolveObjects(GameObject obj, float startValue, float endValue)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        float elapsed = 0f;

        // Inicia el proceso de disolución para todos los renderers simultáneamente
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float lerpFactor = elapsed / duration;
            foreach (var renderer in renderers)
            {
                MaterialPropertyBlock props = new MaterialPropertyBlock();
                float dissolveAmount = Mathf.Lerp(startValue, endValue, lerpFactor);

                renderer.GetPropertyBlock(props);
                props.SetFloat("_DissolveAmount", dissolveAmount);
                renderer.SetPropertyBlock(props);
            }
            yield return null;
        }
    }
}
