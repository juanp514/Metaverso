using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummingbirdCutOff : MonoBehaviour
{
    [SerializeField]
    private Material[] _hummingBirdMaterial = new Material[2];
    private float _cutOffValue = 3f;
    [SerializeField]
    private float _cutOffSpeed;
    [SerializeField]
    private GameObject[] Paths;
    [SerializeField]
    private GameObject Hand;
    [SerializeField]
    private GameObject[] Colliders;
    [SerializeField]
    private float StartTime;
    void Awake()
    {
        //_hummingBirdMaterial[0].SetFloat("_DissolveAmount", 10f);
        //_hummingBirdMaterial[1].SetFloat("_DissolveAmount", 10f);
        
    }

    public void Path()
    {
        Invoke("ChangePaths", StartTime);
        Hand.SetActive(false);
        Paths[0].SetActive(false);
        Colliders[0].SetActive(false);
    }


    public void ChangePaths()
    {
        Paths[2].SetActive(true);
        Paths[1].SetActive(true);
        Colliders[1].SetActive(true);
    }

    void Update()
    {
       // _cutOffValue -= _cutOffSpeed;
       // _hummingBirdMaterial[0].SetFloat("_DissolveAmount", _cutOffValue);
       // _hummingBirdMaterial[1].SetFloat("_DissolveAmount", _cutOffValue);
       // if (_cutOffValue <= 0f)
        //    enabled = false;
    }
}
