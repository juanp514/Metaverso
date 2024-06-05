using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Xennial.TaskManager;

public class ObjectsBehaviour : MonoBehaviour
{
    [SerializeField]
    public AudioClip audio;
    [SerializeField]
    public AudioSource[] _audioSource;
    [SerializeField]
    public List<GameObject> Objects;
    [SerializeField]
    public Light Carriagelight;
    [SerializeField]
    public WaitUntilTask taskToActivate;
    [SerializeField]
    public WaitForSecondsTask _SecondsTaskToActivate;
    [SerializeField]
    public float StartTask = 0f;
    [SerializeField]
    public float NextTask = 0.5f;
    [SerializeField]
    public float duration = 0.5f;
    [SerializeField]
    public float startValueDissolve = 0.78f;
    [SerializeField]
    public float endValueDissolve = 0.339f;
    [SerializeField]
    public bool DeactivateObjectOnEnd;
    [SerializeField]
    public bool CompleteTaskOnEnd;

    [HideInInspector]
    public bool On = false;

    protected virtual void Start()
    {
        if (taskToActivate != null)
            taskToActivate.OnPreExecutionEventHandler += ActivateDissolveEffect;
        else if (_SecondsTaskToActivate != null)
            _SecondsTaskToActivate.OnPreExecutionEventHandler += ActivateDissolveEffect;
    }

    protected virtual void ActivateDissolveEffect()
    {      
        StartCoroutine(ProcessChandeliersSequentially());
    }

    protected virtual IEnumerator ProcessChandeliersSequentially()
    {
        yield return new WaitForSeconds(StartTask);
        foreach (var chandelier in Objects)
        {
            chandelier.SetActive(true);
            if (audio != null)
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    _audioSource[i].PlayOneShot(audio);
                }
            }           
            yield return StartCoroutine(DissolveChandelier(chandelier));
        }

        if (CompleteTaskOnEnd)
        {
            Invoke("FinishTask",NextTask);
        }      
    }

    protected virtual IEnumerator DissolveChandelier(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        float currentDuration = 0f;

        while (currentDuration < duration)
        {
            float t = currentDuration / duration;
            float AumentLight = Mathf.Lerp(0, 9, t);

            if(Carriagelight != null && On == false)
            {
                Carriagelight.intensity = AumentLight;
            }

            foreach (var renderer in renderers)
            {
                MaterialPropertyBlock props = new MaterialPropertyBlock();
                float dissolveAmount = Mathf.Lerp(startValueDissolve, endValueDissolve, t);

                renderer.GetPropertyBlock(props);
                props.SetFloat("_DissolveAmount", dissolveAmount);
                renderer.SetPropertyBlock(props);
            }

            currentDuration += Time.deltaTime;
            yield return null;
        }

        On = true;

        foreach (var renderer in renderers)
        {
            MaterialPropertyBlock props = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(props);
            props.SetFloat("_DissolveAmount", endValueDissolve);
            renderer.SetPropertyBlock(props);
        }

        if(DeactivateObjectOnEnd)
            obj.SetActive(false);
    }

    public void FinishTask()
    {
        if(taskToActivate !=null)
            taskToActivate.CompleteTask();
        else if(_SecondsTaskToActivate != null)
            _SecondsTaskToActivate.CompleteTask();
    }

}
