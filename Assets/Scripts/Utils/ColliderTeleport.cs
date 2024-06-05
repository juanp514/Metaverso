using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTeleport : MonoBehaviour
{
    public delegate void TeleportAction();
    public static event TeleportAction ChangeParameters;

    public delegate void ExitTeleportZone();
    public static event ExitTeleportZone Exit;


      private void OnTriggerEnter(Collider other)
      {
        if(other.gameObject.CompareTag("Player"))
          ChangeParameters();
      }

      private void OnTriggerExit(Collider other)
      {
        if (other.gameObject.CompareTag("Player"))
            Exit();
      }
}
