using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;


    private bool isPlaying = false;

    private void Start()
    {
        ResetAnimation();
    }
    private void OnTriggerEnter(Collider other)
    {
        anim.enabled = true;
        isPlaying = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlaying = false;
        ResetAnimation();
    }

    private void ResetAnimation()
    {
        anim.Rebind();
        anim.Update(0f);
        anim.enabled = false;
    }
}
