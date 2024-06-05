using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearDrawsCollider : MonoBehaviour
{
    public GameObject[] objectDraw;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < objectDraw.Length; i++)
            {
                objectDraw[i].SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < objectDraw.Length; i++)
            {
                objectDraw[i].SetActive(false);
            }
        }
    }
}
