using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _instructionsCanvas;
    private ThemeSelector _themeSelector;
    private bool _isInstructionsShown = true;
    public bool QuizStarted = false;
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
        if (QuizStarted)
        {
            // Start the quiz
            Debug.Log("Quiz started!");
            SetupQuiz setupQuiz = FindObjectOfType<SetupQuiz>();
            int timeToAnswer = setupQuiz.answerTime;
            string difficulty = setupQuiz.difficulty;
            string category = setupQuiz.category;
            bool narrator = setupQuiz.narrator;

            PlayQuiz playQuiz = FindObjectOfType<PlayQuiz>();
            playQuiz.PlayQuizWithSettings(timeToAnswer, difficulty, category, narrator);
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
