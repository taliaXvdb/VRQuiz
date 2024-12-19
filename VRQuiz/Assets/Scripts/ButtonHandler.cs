using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    private Vector3 _initialPosition;
    private ConfigurableJoint _joint;

    public int chosenAnswer;

    void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _initialPosition = transform.localPosition;
    }

    public void OnButtonPressed()
    {
        PlayQuiz playQuiz = FindObjectOfType<PlayQuiz>();

        if (gameObject.name == "BlueButton")
        {
            Debug.Log("Blue Button Pressed!");
            chosenAnswer = 1;
            playQuiz.Answer = chosenAnswer;
        }
        else if (gameObject.name == "GreenButton")
        {
            Debug.Log("Green Button Pressed!");
            chosenAnswer = 2;
            playQuiz.Answer = chosenAnswer;
        }
        else if (gameObject.name == "RedButton")
        {
            Debug.Log("Red Button Pressed!");
            chosenAnswer = 3;
            playQuiz.Answer = chosenAnswer;
        }
        else if (gameObject.name == "YellowButton")
        {
            Debug.Log("Yellow Button Pressed!");
            chosenAnswer = 4;
            playQuiz.Answer = chosenAnswer;
        }
    }

}
