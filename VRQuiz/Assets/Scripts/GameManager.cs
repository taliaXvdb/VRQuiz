using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _instructionsCanvas;
    private ThemeSelector _themeSelector;
    private bool _isInstructionsShown = true;
    void Start()
    {
        ShowInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isInstructionsShown)
        {
            _themeSelector = FindObjectOfType<ThemeSelector>();
            if (_themeSelector != null)
            {
                _themeSelector.enabled = true;
            }
        }
    }

    // Show the instructions canvas for 10 seconds
    public void ShowInstructions()
    {
        _instructionsCanvas.gameObject.SetActive(true);
        StartCoroutine(HideInstructions());
    }

    // Hide the instructions canvas after 10 seconds
    private IEnumerator HideInstructions()
    {
        yield return new WaitForSeconds(10);
        _instructionsCanvas.gameObject.SetActive(false);
        _isInstructionsShown = false;
    }
}
