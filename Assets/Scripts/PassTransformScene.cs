using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassTransformScene : MonoBehaviour
{
    // Variable estática para almacenar el componente Transform que viene de otra escena
    public static PassTransformScene Instance { get; private set; }
    public Transform transforToPass;
    public GameObject gOToPass;

    void Awake()
    {
        // Mantener este GameObject activo entre escenas
        if (Instance == null)
            Instance = this;
    }
}
