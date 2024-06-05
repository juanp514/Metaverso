using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCarriage : MonoBehaviour
{
    [SerializeField]
    private CarriageMovement controlador; 
    [SerializeField]
    private Animator[] Wheels;

    private void OnTriggerEnter(Collider other)
    {
        // Asume que el objeto colisionado tiene el tag "Objetivo"
        if (other.CompareTag("Stop"))
        {
            controlador.Finish();
            foreach(Animator anim in Wheels)
            {
                anim.enabled = false;
            }
            
        }
    }
}
