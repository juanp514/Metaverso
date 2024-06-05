using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiiderHummingbird : MonoBehaviour
{
    [SerializeField]
    private GameObject[] BGcurve;
    [SerializeField]
    private GameObject Hand;
    [SerializeField]
    private Animator HummingBird;
    [SerializeField]
    private HummingBirdDisappear End;
    [SerializeField]
    private float tiempoquieto;
    [SerializeField]
    private GameObject HummingBirdInteractor;

    private void OnTriggerEnter(Collider obj)
    {

        if (obj.CompareTag("Stop"))
        {
            StartCoroutine(deactivateTemporalmente());
            Debug.Log("Stop");
        }
        if (obj.CompareTag("InitialStop"))
        {
            StartCoroutine(TouchdeactivateTemporalmente());
        }

        if (obj.CompareTag("Change"))
        {
            StartCoroutine(ChangeRouteTemporalmente());
        }
        if (obj.CompareTag("Change2"))
        {
            StartCoroutine(ChangeRouteTemporalmente2());
        }
        if (obj.CompareTag("Static"))
        {
            HummingBird.SetBool("Change", true);
        }
        if (obj.CompareTag("Last"))
        {
            HummingBird.SetBool("Change", false);
        }
        if (obj.CompareTag("EndPath"))
        {
            BGcurve[0].SetActive(false);
            End.enabled = true;
            Invoke("FinalDestroy", 1f);
        }
    }

    IEnumerator deactivateTemporalmente()
    {

        BGcurve[0].SetActive(false);

            yield return new WaitForSeconds(tiempoquieto);

        BGcurve[0].SetActive(true);
    }
    IEnumerator TouchdeactivateTemporalmente()
    {

        BGcurve[0].SetActive(false);
        Hand.SetActive(true);

        yield return new WaitForSeconds(10);

        BGcurve[0].SetActive(true);
        Hand.SetActive(false);
    }

    IEnumerator ChangeRouteTemporalmente()
    {

        BGcurve[0].SetActive(false);
        HummingBird.SetBool("Change", true);


        yield return new WaitForSeconds(4);


        BGcurve[0].SetActive(true);
        HummingBird.SetBool("Change", false);
    }

    IEnumerator ChangeRouteTemporalmente2()
    {

        BGcurve[0].SetActive(false);
        HummingBird.SetBool("Change", true);


        yield return new WaitForSeconds(11);


        BGcurve[0].SetActive(true);
        HummingBird.SetBool("Change", false);
    }

    void FinalDestroy()
    {
        HummingBirdInteractor.SetActive(false);
    }

}
