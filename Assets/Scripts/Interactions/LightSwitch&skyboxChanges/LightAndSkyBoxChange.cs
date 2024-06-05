using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Xennial.TaskManager;


namespace LSS
{
    public class LightAndSkyBoxChange : MonoBehaviour
    {
        [SerializeField]
        private WaitUntilTask taskToActivate;
        [SerializeField]
        private float TimeToStart = 4f;
        [SerializeField]
        private VideoPlayer VideoSkyBox;
        [SerializeField]
        private Light[] Lights;
        [SerializeField]
        private GameObject RealTimeLight;
        [SerializeField]
        private Material skyboxMat;
        [SerializeField]
        public float durationFadeInOut = 0.5f;
        [SerializeField]
        public float durationSkyBox = 0.5f;
        private void Awake()
        {
            SetInitialConditions();
        }

        private void SetInitialConditions()
        {
            skyboxMat.SetFloat("_Exposure", 0f);
            Lights[0].intensity = 0;
            Lights[1].intensity = 0.71f;
        }
        private void Start()
        {
            VideoSkyBox.Prepare();
            taskToActivate.OnPreExecutionEventHandler += TriggerChanges;
        }

        private void TriggerChanges()
        {
            Invoke("StartDay", TimeToStart);         
        }

        void StartDay()
        {
            StartCoroutine(TriggerChangesNumerator());
            VideoSkyBox.Play();
            StartCoroutine(Triggerexposure());
            taskToActivate.CompleteTask();
        }

        IEnumerator Triggerexposure()
        {
            float currentDuration = 0f;
            

            while (currentDuration < durationFadeInOut)
            {
                float t = currentDuration / durationFadeInOut;
                float fade = Mathf.Lerp(0, 1, t);
                skyboxMat.SetFloat("_Exposure", fade);

                currentDuration += Time.deltaTime;
                yield return null;
            }

            skyboxMat.SetFloat("_Exposure", 1f);
        }

        IEnumerator TriggerChangesNumerator()
        {
            float currentDuration = 0f;
            Quaternion initialRotation = RealTimeLight.transform.rotation;

            while (currentDuration < durationSkyBox)
            {
                float t = currentDuration / durationSkyBox;
                float LightOut = Mathf.Lerp(0.71f, 0f, t);
                float LightIn = Mathf.Lerp(0, 1.8f, t);
                float Lightin2 = Mathf.Lerp(0f, 13f, t);
                float LightIn3 = Mathf.Lerp(0, 18f, t);
                float LightOut2 = Mathf.Lerp(9f, 0f, t);
                float newAngleX = Mathf.Lerp(-120f, 120f, t);
                Lights[0].intensity = LightIn;
                Lights[1].intensity = LightOut;
                Lights[2].intensity = Lightin2;
                Lights[3].intensity = LightIn3;
                Lights[4].intensity = LightOut2;
                Quaternion newRotationX = Quaternion.Euler(newAngleX, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);
                RealTimeLight.transform.rotation = newRotationX;

                currentDuration += Time.deltaTime;
                yield return null;
            }

            VideoSkyBox.Stop();
            Lights[0].intensity = 1.8f;
            Lights[1].enabled = false;
            Lights[2].intensity = 13;
            Lights[3].intensity = 18;
            Lights[4].enabled = false;
        }
    }
}
