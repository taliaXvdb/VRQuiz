using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Transform controller;
    [SerializeField] private InputActionAsset inputAction;

    private bool _isPressed;
    private Vector3 _startPosition;
    private ConfigurableJoint _joint;
    private InputAction buttonAction;

    public UnityEvent OnPressed, OnReleased;

    void OnEnable()
    {
        // Get the action from the input asset
        var actionMap = inputAction.FindActionMap("Controller");
        buttonAction = actionMap.FindAction("Primary Button");

        buttonAction.Enable();
    }

    void OnDisable()
    {
        buttonAction.Disable();
    }

    void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        Ray ray = new Ray(controller.position, controller.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.Log("ButtonHandler Update");
            if (buttonAction.triggered)
            {
                Debug.Log("Button pressed");
                if (!_isPressed)
                {
                    Pressed();
                }
                else
                {
                    Released();
                }
            }
        }
    }

    private void Pressed()
    {
        _isPressed = true;
        OnPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        _isPressed = false;
        OnReleased.Invoke();
        Debug.Log("Released");
    }
}
