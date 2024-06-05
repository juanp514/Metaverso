using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class KeyboardOVR : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private bool isShift = false;
    [SerializeField] private Image shiftLED;
    [SerializeField] private Color activateColor;
    [SerializeField] private Color disabledColor;

    private bool firstLetter = true;
    //30D13C green
    //525252 gray;

    private void OnEnable()
    {
        ChangeShiftState();
    }
    public void InsertChar(string c) {
        string letter = c;
        letter = CheckShift(c);
        inputField.text += letter;
        if (firstLetter)
        {
            firstLetter = false;
            ChangeShiftState();
        }
    }
    public void DeleteChar(){
        if (inputField.text.Length>0) {
            inputField.text = inputField.text.Substring(0,inputField.text.Length-1);
        }
    
    }

    public void InsertSpace() {

        inputField.text += " ";
    }

    public string CheckShift(string c)
    {
        string letter=c;
        if (isShift)
        {
            letter = c.ToUpper();
        }
        else
        {
            letter = c.ToLower();
        }
        return letter;
    }

    public void ChangeShiftState()
    {
        Debug.Log(isShift);
        isShift = !isShift;
        ChangeColorShift();
    }

    private void ChangeColorShift()
    {
        Color c;
        if (isShift)
        {
            c = activateColor;
        }
        else
        {
            c = disabledColor;
        }
        shiftLED.color = c;
    }
}
