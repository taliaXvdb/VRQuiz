using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupQuiz : MonoBehaviour
{
    public int answerTime = 10;
    public string difficulty = "Normal";
    public string category = "";
    public bool narrator = false;
    [SerializeField] private Transform player; // Reference to the player or camera
    [SerializeField] private Transform teleportDestination; // Reference to the teleport location

    public void SetAnswerTime(int time)
    {
        answerTime = time;
        Debug.Log($"Answer time set to {answerTime} seconds.");
    }

    public void SetDifficulty(string diff)
    {
        difficulty = diff;
        Debug.Log($"Difficulty set to {difficulty}.");
    }

    public void SetNarrator(bool narr)
    {
        narrator = narr;
        Debug.Log($"Narrator set to {(narrator ? "on" : "off")}.");
    }

    public void StartQuiz()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.QuizStarted = true;
        player.position = teleportDestination.position;
        Debug.Log($"Starting quiz with {answerTime} seconds, {difficulty} difficulty, and {(narrator ? "narrator" : "no narrator")}.");
    }

}
