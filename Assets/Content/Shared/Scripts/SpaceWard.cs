using Meta.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWard : MonoBehaviour
{
    private void Start()
    {
        MetaXRSpaceWarp.SetSpaceWarp(true);
    }
}
