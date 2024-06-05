using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Xennial.XR.UI;

public class RadialIndicatorManager : MonoBehaviour
{
    [Header("LoadingBar")]
    [Tooltip("Reference to the circular image")]
    [SerializeField]
    private Image _progressBar;
    [Tooltip("Time of the countDown on seconds")]
    [SerializeField]
    private float _countdownTime = 1.0f;

    private Coroutine _countdownCoroutine;
    private TabletBase _tabletBase;
    private Transform _tabletPos;

    private void Start()
    {
        _tabletBase = gameObject.GetComponent<TabletBase>();
    }

    // Method to start the countDown
    public void StartCountdown(Transform tabletPos)
    {
        _tabletPos = tabletPos;

        if (_countdownCoroutine == null && !_tabletBase.TabletContent.activeSelf)
        {
            _progressBar.enabled = true;

            _countdownCoroutine = StartCoroutine(RunCountdown());
        }
            
    }

    // Coroutine that manages the countDown
    private IEnumerator RunCountdown()
    {
        _progressBar.fillAmount = 0.0f;

        float remainingTime = _countdownTime;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            _progressBar.fillAmount = Mathf.Clamp01(1.0f - (remainingTime / _countdownTime));
            yield return null;
        }

        _tabletBase.OpenTabletByHand(_tabletPos, true);

        _progressBar.fillAmount = 1.0f;

        InterruptCountdown();
    }

    // Method to cancel the countdown and restart the progress bar
    public void InterruptCountdown()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }

        _progressBar.fillAmount = 0.0f;

        _progressBar.enabled = false;
    }
}
