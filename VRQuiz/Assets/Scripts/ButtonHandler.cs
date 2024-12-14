using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    private bool _isPressed = false;
    private Vector3 _initialPosition;
    private ConfigurableJoint _joint;

    public string chosenAnswer;

    void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (_isPressed)
        {
            transform.localPosition = _initialPosition + new Vector3(0, -0.1f, 0);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition, Time.deltaTime * 8f);
        }
    }

    public void OnButtonPressed()
    {
        Debug.Log("Button Pressed!");
        _isPressed = true;

        Debug.Log(gameObject.name);
        PlayQuiz playQuiz = FindObjectOfType<PlayQuiz>();

        if (gameObject.name == "BlueButton")
        {
            Debug.Log("Blue Button Pressed!");
            chosenAnswer = "Answer 1";
            playQuiz.Answer = chosenAnswer;
            OnButtonReleased();
        }
        else if (gameObject.name == "GreenButton")
        {
            Debug.Log("Green Button Pressed!");
            chosenAnswer = "Answer 2";
            playQuiz.Answer = chosenAnswer;
            OnButtonReleased();
        }
        else if (gameObject.name == "RedButton")
        {
            Debug.Log("Red Button Pressed!");
            chosenAnswer = "Answer 3";
            playQuiz.Answer = chosenAnswer;
        }
        else if (gameObject.name == "YellowButton")
        {
            Debug.Log("Yellow Button Pressed!");
            chosenAnswer = "Answer 4";
            playQuiz.Answer = chosenAnswer;
        }
    }

    public void OnButtonReleased()
    {
        Debug.Log("Button Released!");
        _isPressed = false;
    }
}
