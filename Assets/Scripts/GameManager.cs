using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Tutorial Article for the template of this code: https://gamedevacademy.org/educational-games-math-tutorial/ 
    /// </summary>
    public static GameManager instance;

    public int numberOfProblems = 10;

    public PlayerController player; // player object

    public MathProblems mp;

    public static bool isWinOrLose;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        mp = FindObjectOfType<MathProblems>();
        // set instance to this script.
        instance = this;
    }

    // called when the player enters the correct tube
    void CorrectAnswer()
    {
        numberOfProblems -= 1;
        Debug.Log(numberOfProblems);
        // is this the last problem?
        if (numberOfProblems == 0) {
            Win();
        }
        else
        {
            FindObjectOfType<MathProblems>().newProblems();
            UI.instance.displayAnswers(mp);
        }

            
    }

    public void OnPlayerEnterFuelStation(int fuelStation)
    {
        if (UI.instance.answers[fuelStation].text == mp.correctAnswer.ToString()){
            CorrectAnswer();
        }
        else
        {

        }
    }
    // called when the player enters the incorrect tube
    void IncorrectAnswer()
    {
        player.Crash();
    }

    // called when the player answers all the problems
    void Win()
    {
        isWinOrLose = true;
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(true);
    }
    // called if the remaining time on a problem reaches 0
    void Lose()
    {
        isWinOrLose = true;
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
    }

}
