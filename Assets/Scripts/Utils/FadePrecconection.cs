using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePrecconection : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    float lerpTimer;

    private void Start()
    {
        Color alpha = new Color(0, 0, 0, 0);
        StartCoroutine(DoFadeNumerator(1, alpha));
    }

    public void DoFadeIn(int time = 1)
    {
        StartCoroutine(DoFadeNumerator(time, Color.black));
    }

    private IEnumerator DoFadeNumerator(int time, Color finalColor)
    {
        lerpTimer = 0f;
        Material current = mesh.material;
        var colorTemp = current.color;
        while (lerpTimer < 1)
        {
            colorTemp = Color.Lerp(colorTemp, finalColor, lerpTimer / time);
            current.SetColor("_BaseColor", colorTemp);

            //  Debug.Log(lerpTimer);
            lerpTimer += Time.deltaTime;


            yield return null;
        }
        
    }
}
