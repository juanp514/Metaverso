using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpawn : MonoBehaviour
{
    public static PlaceSpawn Instance;
    //public MyPlayerInstance myPlayerInstance;
    private int spawnPlace;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaceToSpwan(int place)
    {
        Debug.Log("click boton exb");
        spawnPlace = place;
    }

    public int GetSpawnPlace() => spawnPlace;
}
