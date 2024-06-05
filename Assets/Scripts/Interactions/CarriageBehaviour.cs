using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageBehaviour : ObjectsBehaviour
{

    protected override IEnumerator ProcessChandeliersSequentially()
    {
        List<Coroutine> coroutines = new List<Coroutine>();

        // Iniciar todas las corutinas al mismo tiempo
        foreach (var chandelier in Objects)
        {
            Coroutine coroutine = StartCoroutine(DissolveChandelier(chandelier));
            coroutines.Add(coroutine);
            chandelier.SetActive(true);
            _audioSource[0].Play();

        }

        // Espera a que todas las corutinas finalicen
        foreach (var coroutine in coroutines)
        {
            yield return coroutine;
        }

        if (CompleteTaskOnEnd)
        {
            FinishTask();
        }
    }
}
