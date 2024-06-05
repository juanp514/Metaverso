using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xennial.TaskManager;

public class Water_FloorBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip audio;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private WaitUntilTask taskToActivate;
    [SerializeField]
    private GameObject WaterDrop; 
    [SerializeField]
    private float velocidad = 5f; 
    [SerializeField]
    private float tiempoVisible = 2f;
    [SerializeField]
    float startGradientValue = 0.5f;
    [SerializeField]
    float endGradientValue = -1f;

    private bool IsActive;

    private void Start()
    {
        taskToActivate.OnPreExecutionEventHandler += ActivateEffect;
    }

    private void ActivateEffect()
    {
        WaterDrop.SetActive(true);
        IsActive = true;
    }

    private void Update()
    {
        if (IsActive)
        {
            WaterDrop.transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        }        
    }

    public void ProcesarColision(GameObject Water, GameObject floor)
    {
        Water.SetActive(false);
        IsActive = false;

        StartCoroutine(ActivarTemporalmenteMesh(floor));
    }

    IEnumerator ActivarTemporalmenteMesh(GameObject floor)
    {
        MeshRenderer rendererTemp = floor.GetComponent<MeshRenderer>();
        if (rendererTemp != null)
        {
            Material material = rendererTemp.material;
            float currentDuration = 0f;

            if (audio != null)
            {
                _audioSource.PlayOneShot(audio);
            }
            rendererTemp.enabled = true;

            while (currentDuration < tiempoVisible)
            {
                float t = currentDuration / tiempoVisible;
                float gradientValue = Mathf.Lerp(startGradientValue, endGradientValue, t);
                material.SetFloat("_Grandiant_Float", gradientValue);

                currentDuration += Time.deltaTime;
                yield return null;
            }

            // Asegurar que el material se establezca al valor final
            material.SetFloat("_Grandiant_Float", endGradientValue);

            // Espera por el tiempo definido
            yield return new WaitForSeconds(tiempoVisible - currentDuration);

            // Desactiva el MeshRenderer
            rendererTemp.enabled = false;
            taskToActivate.CompleteTask();
        }
    }
}
