using UnityEngine;
using UnityEngine.InputSystem;

public class ThemeSelector : MonoBehaviour
{
    [SerializeField] private Transform controller; // Assign your VR controller
    [SerializeField] private string targetTag = "Category"; // Set your desired tag in the Inspector
    private GameObject lastHoveredObject; // Tracks the last object hovered

    [SerializeField] private Canvas settingsCanvas; // Assign your settings canvas
    [SerializeField] private InputActionAsset inputActions; // Assign your input actions
    private InputAction triggerAction; // Tracks the trigger action

    void OnEnable()
    {
        // Get the action from the input asset
        var actionMap = inputActions.FindActionMap("Controller");
        triggerAction = actionMap.FindAction("Trigger");

        triggerAction.Enable();
    }

    void OnDisable()
    {
        triggerAction.Disable();
    }

    void Update()
    {
        Ray ray = new Ray(controller.position, controller.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 10f))
        {
            GameObject hoveredObject = hit.collider.gameObject;

            // Check if the object has the correct tag
            if (hoveredObject.CompareTag(targetTag))
            {
                // If it's a new object
                if (hoveredObject != lastHoveredObject)
                {
                    lastHoveredObject = hoveredObject;
                    Debug.Log($"Hovering over: {hoveredObject.name}");
                }

            }
            if (triggerAction.triggered)
            {
                Debug.Log("Trigger is pressed");
                ShowSettings(hoveredObject.name);
            }
        }
        else
        {
            // Reset when no object is hit or the object doesn't have the correct tag
            if (lastHoveredObject != null)
            {
                Debug.Log("No longer hovering over anything.");
                lastHoveredObject = null;
            }
        }
    }

    void ShowSettings(string categoryName)
    {
        // Find the child GameObject in the settings canvas by name
        GameObject category = settingsCanvas.transform.Find(categoryName)?.gameObject;

        if (category != null)
        {
            category.SetActive(true);
        }
        else
        {
            Debug.LogError($"Category '{categoryName}' not found in the settings canvas.");
        }
    }
}
