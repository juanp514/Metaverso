using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xennial.TaskManager;

public class CarriageMovement : MonoBehaviour
{
    [SerializeField]
    private WaitUntilTask taskToActivate;
    [SerializeField]
    private GameObject Carriage;
    [SerializeField]
    private GameObject Particles;
    [SerializeField]
    public AudioSource _audioSource;
    [SerializeField]
    private GameObject Collider;
    [SerializeField]
    private float velocidad = 5f;

    private bool IsActive;

    private void Start()
    {
        taskToActivate.OnPreExecutionEventHandler += ActivateEffect;
    }

    private void ActivateEffect()
    {
        IsActive = true;
        Collider.SetActive(true);
        Particles.SetActive(false);
    }

    private void Update()
    {
        if (IsActive)
        {
            Carriage.transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
    }

    public void Finish()
    {
        IsActive = false;
        taskToActivate.CompleteTask();
        _audioSource.Stop();
    }
}
