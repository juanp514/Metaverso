using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummingBirdDisappear : MonoBehaviour
{
    [SerializeField]
    private Material[] _hummingBirdMaterial = new Material[2];
    private float _cutOffValue = 3f;
    [SerializeField]
    private float _cutOffSpeed;

    void Awake()
    {
        _hummingBirdMaterial[0].SetFloat("_DissolveAmount", 10f);
        _hummingBirdMaterial[1].SetFloat("_DissolveAmount", 10f);

    }

    void Update()
    {
         _cutOffValue -= _cutOffSpeed;
         _hummingBirdMaterial[0].SetFloat("_DissolveAmount", _cutOffValue);
         _hummingBirdMaterial[1].SetFloat("_DissolveAmount", _cutOffValue);
         if (_cutOffValue <= 0f)
            enabled = false;
    }
}
