using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayQuiz : MonoBehaviour
{
    public string _category;
    public int _answerTime;
    public string _difficulty;
    public bool _narrator;
    private bool _isInstructionsShown = true;
    private List<Question> _questions;
    private SetupQuiz _setupQuiz;
    private int _currentQuestionIndex = 1;
    private bool _isQuizStarted = false;
    public int Score = 0;
    public int Answer = 0;
    [SerializeField] private Canvas _instructionsCanvas;
    [SerializeField] private Canvas _quizCanvas;
    [SerializeField] private Canvas _scoreCanvas;
    [SerializeField] private Canvas _nextCanvas;
    [SerializeField] private AudioSource _audioSourceWrong;
    [SerializeField] private AudioSource _audioSourceCorrect;
    [SerializeField] private AudioSource _audioSourceCountdown;

    TMP_Text questionTitle;
    TMP_Text questionText;
    TMP_Text answer1;
    TMP_Text answer2;
    TMP_Text answer3;
    TMP_Text answer4;
    TMP_Text scoreTitle;
    TMP_Text score;

    void Start()
    {
        _setupQuiz = FindObjectOfType<SetupQuiz>();
        _setupQuiz.enabled = false;
        _currentQuestionIndex = 1; // Reset to the first question

        questionTitle = _quizCanvas.transform.Find("QuestionTitle").GetComponent<TMP_Text>();
        questionText = _quizCanvas.transform.Find("QuestionText").GetComponent<TMP_Text>();
        answer1 = _quizCanvas.transform.Find("Answer1").GetComponentInChildren<TMP_Text>();
        answer2 = _quizCanvas.transform.Find("Answer2").GetComponentInChildren<TMP_Text>();
        answer3 = _quizCanvas.transform.Find("Answer3").GetComponentInChildren<TMP_Text>();
        answer4 = _quizCanvas.transform.Find("Answer4").GetComponentInChildren<TMP_Text>();

        scoreTitle = _scoreCanvas.transform.Find("ScoreTitle").GetComponent<TMP_Text>();
        score = _scoreCanvas.transform.Find("Score").GetComponent<TMP_Text>();

        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayPlaySound();
        }

        _audioSourceCountdown.Play();
        ShowInstructions();
        PrepareQuestions();

    }

    void Update()
    {
        if (!_isInstructionsShown && _isQuizStarted)
        {
            _quizCanvas.gameObject.SetActive(true);
            _isQuizStarted = false;

            StartQuiz();
        }
    }

    public void PrepareQuestions()
    {
        Debug.Log($"Playing quiz with settings: Answer Time: {_answerTime}, Difficulty: {_difficulty}, Category: {_category}, Narrator: {_narrator}");
        if (_difficulty == "Easy")
        {
            MakeQuestionList("Easy");
            Debug.Log("Easy mode selected.");
        }
        else if (_difficulty == "Normal")
        {
            MakeQuestionList("Normal");
            Debug.Log("Normal mode selected.");
        }
        else if (_difficulty == "Hard")
        {
            MakeQuestionList("Hard");
            Debug.Log("Hard mode selected.");
        }
        else
        {
            Debug.LogWarning("Invalid difficulty selected.");
        }
    }

    void MakeQuestionList(string diff)
    {
        Debug.Log("Making question list...");
        if (diff == "Easy")
        {
            List<Question> questions = new List<Question>
            {
                // Easy Questions
                new Question(
                    "Who is the main character in the Harry Potter series, who goes to a magical school?",
                    new List<string> { "Ron Weasley", "Draco Malfoy", "Hermione Granger", "Harry Potter" },
                    "Harry Potter"
                ),
                new Question(
                    "What book series features Katniss Everdeen fighting in a deadly competition?",
                    new List<string> { "Twilight", "The Hunger Games", "The Maze Runner", "Divergent" },
                    "The Hunger Games"
                ),
                new Question(
                    "Who wrote the Diary of a Wimpy Kid series?",
                    new List<string> { "Dav Pilkey", "Rick Riordan", "Roald Dahl", "Jeff Kinney" },
                    "Jeff Kinney"
                ),
                new Question(
                    "In Percy Jackson and the Olympians, what Greek god is Percy’s father?",
                    new List<string> { "Zeus", "Poseidon", "Apollo", "Hades" },
                    "Poseidon"
                ),
                new Question(
                    "What color is the dress on the cover of the book The Selection?",
                    new List<string> { "Red", "Purple", "Green", "Blue" },
                    "Blue"
                ),
                new Question(
                    "Who wrote The Fault in Our Stars, a book about two teens dealing with cancer?",
                    new List<string> { "Rainbow Rowell", "Nicholas Sparks", "John Green", "Sarah Dessen" },
                    "John Green"
                ),
                new Question(
                    "What’s the title of the book where a spider writes words in her web to save a pig?",
                    new List<string> { "Stuart Little", "Charlotte's Web", "Babe", "Animal Farm" },
                    "Charlotte's Web"
                ),
                new Question(
                    "What book series has the tagline, “One ring to rule them all”?",
                    new List<string> { "Percy Jackson", "The Lord of the Rings", "Harry Potter", "The Chronicles of Narnia" },
                    "The Lord of the Rings"
                ),
                new Question(
                    "In Twilight, what is the name of Bella’s vampire boyfriend?",
                    new List<string> { "Edward Cullen", "Jacob Black", "Jasper Hale", "Emmett Cullen" },
                    "Edward Cullen"
                ),
                new Question(
                    "What is the first book in the Divergent series?",
                    new List<string> { "Allegiant", "Insurgent", "The Hunger Games", "Divergent" },
                    "Divergent"
                ),
            };
            _questions = questions;
        }
        else if (diff == "Normal")
        {
            List<Question> questions = new List<Question>
            {
                new Question(
                    "Who is the author of Looking for Alaska?",
                    new List<string> { "Suzanne Collins", "Stephen King", "John Green", "J.K. Rowling" },
                    "John Green"
                ),
                new Question(
                    "In the Harry Potter series, what house is Draco Malfoy in?",
                    new List<string> { "Gryffindor", "Ravenclaw", "Slytherin", "Hufflepuff" },
                    "Slytherin"
                ),
                new Question(
                    "What is the dystopian city called in The Maze Runner series?",
                    new List<string> { "The Glade", "Panem", "District 12", "The Capitol" },
                    "The Glade"
                ),
                new Question(
                    "Who wrote the novel Eleanor & Park, about two misfit teens falling in love?",
                    new List<string> { "Cassandra Clare", "Rainbow Rowell", "John Green", "Nicholas Sparks" },
                    "Rainbow Rowell"
                ),
                new Question(
                    "In the Hunger Games, what is the name of Katniss’s home district?",
                    new List<string> { "District 13", "District 5", "District 12", "District 1" },
                    "District 12"
                ),
                new Question(
                    "What book follows Starr Carter as she deals with police violence and activism?",
                    new List<string> { "All American Boys", "Dear Martin", "The Hate U Give", "Angie Thomas" },
                    "The Hate U Give"
                ),
                new Question(
                    "In Divergent, what faction values intelligence above all?",
                    new List<string> { "Amity", "Candor", "Erudite", "Dauntless" },
                    "Erudite"
                ),
                new Question(
                    "Who wrote the novel Wonder, about a boy with a facial difference?",
                    new List<string> { "Sarah Weeks", "R.J. Palacio", "John Green", "Jodi Picoult" },
                    "R.J. Palacio"
                ),
                new Question(
                    "What is the title of the first book in A Series of Unfortunate Events?",
                    new List<string> { "The Austere Academy", "The Reptile Room", "The Wide Window", "The Bad Beginning" },
                    "The Bad Beginning"
                ),
                new Question(
                    "What’s the name of the magical wardrobe world in C.S. Lewis’s book?",
                    new List<string> { "Neverland", "Narnia", "Hogwarts", "Middle-earth" },
                    "Narnia"
                ),
            };
            _questions = questions;
        }
        else if (diff == "Hard")
        {
            List<Question> questions = new List<Question>
            {
                new Question(
                    "Who is the author of They Both Die at the End, a book about two teens who meet on their last day alive?",
                    new List<string> { "John Green", "Nicola Yoon", "Adam Silvera", "Becky Albertalli" },
                    "Adam Silvera"
                ),
                new Question(
                    "In Shadow and Bone, what is the name of the dark magical force Alina Starkov faces?",
                    new List<string> { "The Rift", "The Darkness", "The Fold", "The Shadow Realm" },
                    "The Fold"
                ),
                new Question(
                    "What is the real identity of the anonymous blogger in Gossip Girl (book series)?",
                    new List<string> { "Blair Waldorf", "Chuck Bass", "Dan Humphrey", "Serena van der Woodsen" },
                    "Dan Humphrey"
                ),
                new Question(
                    "In Six of Crows, what is Kaz Brekker’s nickname?",
                    new List<string> { "The Thief King", "The Bastard of the Barrel", "Dirtyhands", "The Shadow" },
                    "Dirtyhands"
                ),
                new Question(
                    "Who wrote One of Us Is Lying, a mystery about four teens under suspicion of murder?",
                    new List<string> { "R.L. Stine", "Holly Jackson", "Karen M. McManus", "Sarah Dessen" },
                    "Karen M. McManus"
                ),
                new Question(
                    "What is the name of the boarding school in Crescent City: House of Earth and Blood by Sarah J. Maas?",
                    new List<string> { "Mistwood", "Bryce Quinlan doesn’t attend a school", "The House of Fae", "Velaris Academy" },
                    "Bryce Quinlan doesn’t attend a school"
                ),
                new Question(
                    "In The Cruel Prince, who is Jude’s primary rival in the faerie court?",
                    new List<string> { "Madoc", "Taryn", "Cardan", "Nicasia" },
                    "Cardan"
                ),
                new Question(
                    "What is the name of the group of teen vigilantes in Renegades by Marissa Meyer?",
                    new List<string> { "The Watchers", "The Renegades", "The Enforcers", "The Anarchists" },
                    "The Renegades"
                ),
                new Question(
                    "In Red Queen, what power does Mare Barrow discover she has?",
                    new List<string> { "Shapeshifting", "Healing powers", "Controlling electricity", "Reading minds" },
                    "Controlling electricity"
                ),
                new Question(
                    "Who wrote We Were Liars, a book about a privileged but broken family on a private island?",
                    new List<string> { "Sarah Dessen", "E. Lockhart", "John Green", "Jennifer Niven" },
                    "E. Lockhart"
                ),
            };
            _questions = questions;
        }
    }

    public void StartQuiz()
    {
        Debug.Log("Starting quiz...");
        StartCoroutine(DisplayQuestions());
    }

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

        _isQuizStarted = true;
        _isInstructionsShown = false;
    }

    private void ShowQuestion(Question question)
    {
        questionTitle.text = string.Empty;
        questionText.text = string.Empty;
        answer1.text = string.Empty;
        answer2.text = string.Empty;
        answer3.text = string.Empty;
        answer4.text = string.Empty;

        Debug.Log(question.Text);
        questionTitle.text = "Question " + _currentQuestionIndex;
        questionText.text = question.Text;
        answer1.text = question.Answers[0];
        answer2.text = question.Answers[1];
        answer3.text = question.Answers[2];
        answer4.text = question.Answers[3];
        _currentQuestionIndex++;
    }

    private IEnumerator DisplayQuestions()
    {
        foreach (Question question in _questions)
        {
            ShowQuestion(question);
            yield return new WaitForSeconds(_answerTime);
            CheckAnswer(question);
        }

        Debug.Log("Quiz completed!");
        Debug.Log($"Your score: {Score}/{_questions.Count}");
        _quizCanvas.gameObject.SetActive(false);
        _scoreCanvas.gameObject.SetActive(true);
        if (Score == 10)
        {
            scoreTitle.text = "Perfect!";
            score.text = Score + "/" + _questions.Count;
        }
        else if (Score >= 7)
        {
            scoreTitle.text = "Well done!";
            score.text = Score + "/" + _questions.Count;
        }
        else if (Score >= 4)
        {
            scoreTitle.text = "Good effort!";
            score.text = Score + "/" + _questions.Count;
        }
        else
        {
            scoreTitle.text = "Better luck next time!";
            score.text = Score + "/" + _questions.Count;
        }
    }

    private void CheckAnswer(Question question)
    {
        if (Answer == 0)
        {
            Debug.Log("Incorrect answer.");
            _audioSourceWrong.Play();
            Debug.Log($"Correct answer: {question.CorrectAnswer}");
        }
        else if (question.CorrectAnswer == question.Answers[Answer - 1])
        {
            Debug.Log("Correct answer!");
            _audioSourceCorrect.Play();
            Score++;
        }
        else if (question.CorrectAnswer != question.Answers[Answer - 1])
        {
            Debug.Log("Incorrect answer.");
            _audioSourceWrong.Play();
            Debug.Log($"Correct answer: {question.CorrectAnswer}");
        }
    }

    public void ShowNextOptions()
    {
        Debug.Log("Showing next options...");
        _scoreCanvas.gameObject.SetActive(false);
        _nextCanvas.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        _nextCanvas.gameObject.SetActive(false);
        _isInstructionsShown = true;
        _currentQuestionIndex = 1;
        Score = 0;
        Answer = 0;
        Start();
    }

    public void NewQuiz()
    {
        _nextCanvas.gameObject.SetActive(false);
        _currentQuestionIndex = 1;
        Score = 0;
        Answer = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



public class Question
{
    public string Text { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }

    public Question(string text, List<string> answers, string correctAnswer)
    {
        Text = text;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}