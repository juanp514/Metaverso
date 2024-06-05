using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Transform mainCameraTransform;

    [SerializeField]
    private bool configurateOnAwake = false;
    [SerializeField]
    private float distanceFromPlayer = 1.5f;
    [SerializeField]
    private float rotationSpeed = 5;
    [SerializeField]
    [Range(0f, 90f)]
    private float rotationTolerance = 0.15f;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Vector3 offset=Vector3.zero;
    [SerializeField]
    private Vector3 customScale = new Vector3(-0.75f, 0.75f, 0.75f);
    private Camera mainCameraComponent;
    private bool follow;
    private float negativeXAxisTolerance, possitiveXAxisTolerane;
    private bool isVR = false;
    public float DistanceFromPlayer { get => distanceFromPlayer; set => distanceFromPlayer = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        mainCameraComponent = mainCameraTransform.GetComponent<Camera>();
        StartCoroutine(UpdateTransformEndOfFrame());
        negativeXAxisTolerance = 0.5f - rotationTolerance;
        possitiveXAxisTolerane = 0.5f + rotationTolerance;
    }

    // Update is called once per frame
    void Update()
    {
            SetRotation();
            SetPosition();
    }
    private IEnumerator UpdateTransformEndOfFrame()
    {
        // Wait until the camera has finished the current frame.
        yield return new WaitForEndOfFrame();

        if (configurateOnAwake)
            UpdateMenuTransform(Camera.main);
    }

    private void SetRotation()
    {
        Vector3 targetDir = transform.position- mainCameraTransform.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);


       
    }

    private void SetPosition()
    {
        Vector3 screenPos = mainCameraComponent.WorldToViewportPoint(transform.position);
        float distance = Vector3.Distance(transform.position, mainCameraComponent.transform.position);

        if (screenPos.x > possitiveXAxisTolerane || screenPos.x < negativeXAxisTolerance || distance < DistanceFromPlayer * .95f || distance > DistanceFromPlayer * 1.05)
            follow = true;
        else
            follow = false;


        if (follow)
        {
            Vector3 cameraDirection = mainCameraTransform.forward;
            cameraDirection.y = 0;
            if (configurateOnAwake)
                transform.position = mainCameraTransform.position + offset + cameraDirection;
            else
                transform.position = Vector3.Lerp(transform.position, mainCameraTransform.position + offset + cameraDirection * DistanceFromPlayer, Time.deltaTime * 2f);

        }
    }

    public void UpdateMenuTransform(Camera camera)
    {
        // Move the object in front of the camera with specified offsets.

        Vector3 cameraForward = camera.transform.forward;
        if (cameraForward.z > 0)
            cameraForward.z = 1;
        else
            cameraForward.z = -1;

        Vector3 targetPosition = camera.transform.position + offset + (cameraForward * distanceFromPlayer);
        targetPosition.Set(0, camera.transform.position.y - 0, 0);
        transform.position = targetPosition;

    }

}
