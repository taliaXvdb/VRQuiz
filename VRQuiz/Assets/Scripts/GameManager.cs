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
        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayStopSound();
        }
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
            SetupQuiz setupQuiz = FindObjectOfType<SetupQuiz>();
            int timeToAnswer = setupQuiz.answerTime;
            string difficulty = setupQuiz.difficulty;
            string category = setupQuiz.category;
            bool narrator = setupQuiz.narrator;

            _themeSelector = FindObjectOfType<ThemeSelector>();
            _themeSelector.enabled = false;
            PlayQuiz playQuiz = FindObjectOfType<PlayQuiz>();
            playQuiz.enabled = true;
            playQuiz._answerTime = timeToAnswer;
            playQuiz._difficulty = difficulty;
            playQuiz._category = category;
            playQuiz._narrator = narrator;
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
        yield return new WaitForSeconds(3);
        _instructionsCanvas.gameObject.SetActive(false);

        _isInstructionsShown = false;
    }
}
