using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizChooser : MonoBehaviour
{
    private bool isHovering = false;

    void Update()
    {
        // Raycast from the controller forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (!isHovering)
                {
                    isHovering = true;
                    Debug.Log("Hovering over " + this.gameObject.name);
                }
            }
        }
        else if (isHovering)
        {
            isHovering = false;
        }
    }
}
