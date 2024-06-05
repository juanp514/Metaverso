using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class VidioTimer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform targetQuad;
    [SerializeField] private float videoDuration = 1f; // Duración total del video en segundos

    private void Update()
    {
        // Obtener la dirección del quad desde la cámara
        Vector3 directionToQuad = targetQuad.position - playerCamera.position;

        // Calcular el ángulo entre la dirección de la cámara y la dirección al quad
        float angle = Vector3.Angle(playerCamera.forward, directionToQuad);

        // Convertir el ángulo a un valor entre 0 y 1
        float normalizedAngle = angle / 180f;

        // Asegurar que el valor esté en el rango [0, 1]
        normalizedAngle = Mathf.Clamp01(normalizedAngle);

        // Calcular el tiempo del video basado en el ángulo
        float videoTime = normalizedAngle * videoDuration;

        // Establecer el tiempo del video
        videoPlayer.time = videoTime;
        
        Debug.Log($"Time {videoTime}");

        // Pausar el video para mantenerlo en el tiempo específico
        videoPlayer.Pause();
    }
}
