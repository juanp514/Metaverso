using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xennial.TaskManager;

public class SkyAndCloudsBehaviour : MonoBehaviour
{
    public Material skyboxMaterial;  // Asigna el material Skybox en el inspector
   // public Material cloudsMaterial;  // Asigna el material de las nubes en el inspector
    public float _duration = 5.0f;  // Duraci�n de la transici�n en segundos
    [SerializeField]
    public float NextTask = 0.5f;
    [SerializeField]
    public WaitForSecondsTask _SecondsTaskToActivate;
    [SerializeField]
    public bool CompleteTaskOnEnd;

    [HideInInspector]
    public bool On = false;

    private void Awake()
    {
        skyboxMaterial.SetFloat("_Exposure", 1);
    }

    private void Start()
    {
        _SecondsTaskToActivate.OnPreExecutionEventHandler += ActivateTransition;
    }

    protected virtual void ActivateTransition()
    {
        StartCoroutine(ChangeColorsToWhite());

        if (CompleteTaskOnEnd)
        {
            Invoke("FinishTask", NextTask);
        }
    }

    IEnumerator ChangeColorsToWhite()
    {
        float currentTime = 0.0f;
       // Color originalCloudsFrontColor = cloudsMaterial.GetColor("_Front_Color"); // Asume que este es el nombre del par�metro de color en las nubes
        //Color originalCloudsBackColor = cloudsMaterial.GetColor("_Back_Color"); // Asume que este es el nombre del par�metro de color en las nubes
       // Color originalCloudsColor1 = cloudsMaterial.GetColor("_Color1"); // Asume que este es el nombre del par�metro de color en las nubes
       // Color originalCloudsColor2 = cloudsMaterial.GetColor("_Color2"); // Asume que este es el nombre del par�metro de color en las nubes

        while (currentTime < _duration)
        {
            float t = currentTime / _duration; // Normalizar tiempo transcurrido
            float newSkyboxColor = Mathf.Lerp(1, 4, t);
          //  Color newCloudsFrontColor = Color.Lerp(originalCloudsFrontColor, Color.white, t);
          //  Color newCloudsBackColor = Color.Lerp(originalCloudsBackColor, Color.white, t);
          //  Color newCloudsColor1 = Color.Lerp(originalCloudsColor1, Color.white, t);
          //  Color newCloudsColor2 = Color.Lerp(originalCloudsColor2, Color.white, t);

            skyboxMaterial.SetFloat("_Exposure", newSkyboxColor);
          //  cloudsMaterial.SetColor("_Front_Color", newCloudsFrontColor);  // Aseg�rate de que "_FrontColor" es el correcto
          //  cloudsMaterial.SetColor("_Back_Color", newCloudsBackColor);  // Aseg�rate de que "_FrontColor" es el correcto
          //  cloudsMaterial.SetColor("_Color1", newCloudsColor1);  // Aseg�rate de que "_FrontColor" es el correcto
          //  cloudsMaterial.SetColor("_Color2", newCloudsColor2);  // Aseg�rate de que "_FrontColor" es el correcto

            currentTime += Time.deltaTime;
            yield return null; // Esperar hasta el pr�ximo frame

        }

        skyboxMaterial.SetFloat("_Exposure", 4);
      //  cloudsMaterial.SetColor("_Front_Color", Color.white);
     //   cloudsMaterial.SetColor("_Back_Color", Color.white);
     //   cloudsMaterial.SetColor("_Color1", Color.white);
      //  cloudsMaterial.SetColor("_Color2", Color.white);
    }

    public void FinishTask()
    {
        _SecondsTaskToActivate.CompleteTask();
    }
}
