using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Xennial.Services;
using Xennial.XR.Player;
using Xennial.XR.UI;

public class ButtonListener : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Objects that are goin to be enable on click")]
    [SerializeField]
    private GameObject[] _objectsToEnable;
    [Tooltip("Objects that are goin to be Disable on click")]
    [SerializeField]
    private GameObject[] _objectsToDisable;
    [Tooltip("If true on click button close Tablet")]
    [SerializeField]
    private bool _CloseTablet;
    [Tooltip("script to close properly the tablet")]
    [SerializeField]
    private TabletBase _tabletBase;

    [Header("Haptic")]
    [Tooltip("script that Know if its left or right hand that poke the button")]
    [SerializeField]
    private HapticOnClick _hapticOnClick;
    [Tooltip("Set The intensity of the haptic feedback")]
    [SerializeField]
    private float _intensity = 0.5f;
    [Tooltip("Set the duration of the haptic feedback")]
    [SerializeField]
    public float _duration = 0.2f;

    [Header("Scene")]
    [Tooltip("index to the scene that is going to be load if is -1 none scene should be loaded on click")]
    [SerializeField]
    private int _sceneBuildIndex = -1;
    [Tooltip("If true on click button reset level")]
    [SerializeField]
    private bool _ResetLevel = false;
    [Tooltip("If true on click button quit Game")]
    [SerializeField]
    private bool _QuitGame = false;

    private Button _YourButton;

    private XRBaseController _leftControllerHaptic;
    private XRBaseController _rightControllerHaptic;

    void Start()
    {
        _YourButton = GetComponent<Button>();

        _leftControllerHaptic = _tabletBase.LeftController.GetComponent<XRBaseController>();
        _rightControllerHaptic = _tabletBase.RightController.GetComponent<XRBaseController>();

        if (_YourButton != null)
            _YourButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (_hapticOnClick.LeftController)
            _leftControllerHaptic.SendHapticImpulse(_intensity, _duration);
        else if (_hapticOnClick.RightController)
            _rightControllerHaptic.SendHapticImpulse(_intensity, _duration);

        ToggleObjects(_objectsToEnable, true);
        ToggleObjects(_objectsToDisable, false);

        if (_CloseTablet)
            CloseTablet();

        if (_sceneBuildIndex != -1)
            ChangeSceneByIndex();

        if (_ResetLevel)
            ResetCurrentScene();

        if (_QuitGame)
            QuitGame();
    }

    // Método para activar/desactivar objetos
    private void ToggleObjects(GameObject[] objects, bool state)
    {
        if (objects != null)
        {
            foreach (GameObject obj in objects)
            {
                if (obj != null) obj.SetActive(state);
            }
        }
    }

    private void CloseTablet()
    {
        _tabletBase.DeactivateTablet();
    }

    // Method to change scene By index
    private void ChangeSceneByIndex()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ServiceLocator.Instance.Get<Player>().FadeZone.FadeIn(() => SceneManager.LoadScene(_sceneBuildIndex));
    }

    // Méthod to restart Scene
    private void ResetCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentSceneIndex);
        if (currentSceneIndex != -1)
            ServiceLocator.Instance.Get<Player>().FadeZone.FadeIn(() => SceneManager.LoadScene(currentSceneIndex));
    }

    // Method to quit the game
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
