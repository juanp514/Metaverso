using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearSceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransportButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            sceneTransportButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            sceneTransportButton.SetActive(false);
    }
}
