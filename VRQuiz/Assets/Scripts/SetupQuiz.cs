using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupQuiz : MonoBehaviour
{
    [SerializeField] private int answerTime = 10;
    [SerializeField] private string difficulty = "normal";
    public string category = "";
    [SerializeField] private bool narrator = false;

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
        Debug.Log($"Starting quiz with {answerTime} seconds, {difficulty} difficulty, and {(narrator ? "narrator" : "no narrator")}.");
    }

}
