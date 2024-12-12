using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClicked(GameObject button)
    {
        // Identify the clicked button and trigger the corresponding action
        Debug.Log($"Button {button.name} clicked!");

        switch (button.name)
        {
            case "ButtonBlue":
                Debug.Log("Action for Button 1");
                break;
            case "ButtonGreen":
                Debug.Log("Action for Button 2");
                break;
            case "ButtonRed":
                Debug.Log("Action for Button 3");
                break;
            case "ButtonYellow":
                Debug.Log("Action for Button 4");
                break;
        }
    }
}
