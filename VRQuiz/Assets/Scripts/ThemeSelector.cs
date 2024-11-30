using UnityEngine;
using UnityEngine.InputSystem;

public class ThemeSelector : MonoBehaviour
{
    [SerializeField] private Transform controller; // Assign your VR controller
    [SerializeField] private string targetTag = "Category"; // Set your desired tag in the Inspector
    private GameObject lastHoveredObject; // Tracks the last object hovered

    [SerializeField] private Canvas settingsCanvas; // Assign your settings canvas
    [SerializeField] private Canvas hoverCanvas; // Assign your quiz canvas
    [SerializeField] private InputActionAsset inputActions; // Assign your input actions
    private InputAction buttonAction; // Tracks the trigger action

    void OnEnable()
    {
        // Get the action from the input asset
        var actionMap = inputActions.FindActionMap("Controller");
        buttonAction = actionMap.FindAction("Primary Button");

        buttonAction.Enable();
    }

    void OnDisable()
    {
        buttonAction.Disable();
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
                    ShowHoverCanvas(hoveredObject.name);
                }

            }
            else
            {
                // Reset when the object doesn't have the correct tag
                if (lastHoveredObject != null)
                {
                    Debug.Log("No longer hovering over anything.");
                    lastHoveredObject = null;
                    HideHoverCanvas();
                }
            }
            if (buttonAction.triggered)
            {
                Debug.Log("Activate is pressed");
                HideHoverCanvas();
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
                HideHoverCanvas();
            }
        }
    }

    void ShowSettings(string categoryName)
    {
        // Find the child GameObject in the settings canvas by name
        GameObject category = settingsCanvas.transform.Find(categoryName)?.gameObject;
        SetupQuiz setupQuiz = FindObjectOfType<SetupQuiz>();

        if (category != null)
        {
            category.SetActive(true);
            setupQuiz.category = categoryName;
        }
        else
        {
            Debug.LogError($"Category '{categoryName}' not found in the settings canvas.");
        }
    }

    void ShowHoverCanvas(string categoryName)
    {
        // Find the child GameObject in the hover canvas by name
        GameObject category = hoverCanvas.transform.Find(categoryName)?.gameObject;

        if (category != null)
        {
            category.SetActive(true);
        }
        else
        {
            Debug.LogError($"Category '{categoryName}' not found in the hover canvas.");
        }
    }

    void HideHoverCanvas()
    {
        foreach (Transform child in hoverCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
