using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartAutomatically : MonoBehaviour
{
    [SerializeField] private UnityEvent offLineEvent;

    // Start is called before the first frame update
    void Start()
    {
        offLineEvent.Invoke();
    }
}
