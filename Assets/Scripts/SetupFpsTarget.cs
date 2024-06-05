using UnityEngine;

public class SetupFpsTarget : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 90;
    }
}
