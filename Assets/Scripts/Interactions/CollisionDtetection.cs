using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDtetection : MonoBehaviour
{
    [SerializeField]
    private Water_FloorBehaviour controlador; // Asigna esto en el Inspector
    [SerializeField]
    private GameObject VFXfloor;

    private void OnTriggerEnter(Collider other)
    {
        // Asume que el objeto colisionado tiene el tag "Objetivo"
        if (other.CompareTag("Floor"))
        {
            controlador.ProcesarColision(gameObject, VFXfloor);
        }
    }
}
