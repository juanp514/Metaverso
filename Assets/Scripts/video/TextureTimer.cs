using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTimer : MonoBehaviour
{
    public Texture[] images;
    public Transform playerCamera;
    public float sensitivity = 1.0f; // Factor de sensibilidad para aumentar la velocidad del efecto
    public float maxAngle = 60f; // �ngulo m�ximo de la cabeza para cambiar entre frames
    public AnimationCurve interpolationCurve; // Curva de interpolaci�n para suavizar la transici�n

    private Renderer quadRenderer;

    void Start()
    {
        playerCamera = Camera.main.transform;
        quadRenderer = GetComponent<Renderer>();
        if (quadRenderer == null)
        {
            Debug.LogError("El objeto no tiene un componente Renderer adjunto.");
            return;
        }
        Vector3 forward = transform.forward;
        forward.x += 0.2f;
    }

    void Update()
    {
        UpdateParallax();
    }

    void UpdateParallax()
    {
        if (quadRenderer == null)
            return;

        // Vector que va del objeto (en este caso, el objeto que lleva la c�mara) a la c�mara
        Vector3 toCamera = playerCamera.position - transform.position;

        // Vector de frente del objeto en el espacio mundial
        Vector3 forward = transform.forward;
        forward.x += 0.2f;

        // Calculamos el �ngulo entre el vector de frente del objeto y el vector que va al jugador
        float angle = Vector3.SignedAngle(forward, toCamera, transform.up);

        // Ajustamos el �ngulo para que est� entre -maxAngle y maxAngle
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        // Convertimos el �ngulo a un valor en el rango [0, 1], multiplicado por la sensibilidad
        float normalizedAngle = Mathf.InverseLerp(-maxAngle, maxAngle, angle) * sensitivity;

        // Usamos la curva de interpolaci�n para suavizar la transici�n entre frames
        float lerpValue = interpolationCurve.Evaluate(normalizedAngle);

        // Calculamos el �ndice de la imagen en base al �ngulo interpolado
        int imageIndex = Mathf.RoundToInt(lerpValue * (images.Length - 1));

        // Aplicamos el �ndice de imagen al material del objeto
        quadRenderer.material.mainTexture = images[imageIndex];
    }
}
