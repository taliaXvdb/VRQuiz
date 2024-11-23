using UnityEngine;

public class ThemeSelector : MonoBehaviour
{
    [SerializeField] private Transform controller; // Assign your VR controller
    [SerializeField] private string targetTag = "Category"; // Set your desired tag in the Inspector
    private GameObject lastHoveredObject; // Tracks the last object hovered

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

                    // Log the object's name or other details
                    Debug.Log($"Hovering over: {hoveredObject.name}");
                }
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
}
