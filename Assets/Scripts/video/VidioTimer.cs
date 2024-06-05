using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class VidioTimer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform targetQuad;
    [SerializeField] private float videoDuration = 1f; // Duraci�n total del video en segundos

    private void Update()
    {
        // Obtener la direcci�n del quad desde la c�mara
        Vector3 directionToQuad = targetQuad.position - playerCamera.position;

        // Calcular el �ngulo entre la direcci�n de la c�mara y la direcci�n al quad
        float angle = Vector3.Angle(playerCamera.forward, directionToQuad);

        // Convertir el �ngulo a un valor entre 0 y 1
        float normalizedAngle = angle / 180f;

        // Asegurar que el valor est� en el rango [0, 1]
        normalizedAngle = Mathf.Clamp01(normalizedAngle);

        // Calcular el tiempo del video basado en el �ngulo
        float videoTime = normalizedAngle * videoDuration;

        // Establecer el tiempo del video
        videoPlayer.time = videoTime;
        
        Debug.Log($"Time {videoTime}");

        // Pausar el video para mantenerlo en el tiempo espec�fico
        videoPlayer.Pause();
    }
}
