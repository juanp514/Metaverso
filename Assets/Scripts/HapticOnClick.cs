using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Xennial.XR.UI;

public class HapticOnClick : MonoBehaviour
{
    private bool _leftControllerPoke = false;
    private bool _rightControllerPoke = false;

    public bool LeftController { get => _leftControllerPoke; set => _leftControllerPoke = value; }
    public bool RightController { get => _rightControllerPoke; set => _rightControllerPoke = value; }

}
