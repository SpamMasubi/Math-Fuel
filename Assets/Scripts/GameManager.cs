using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int numberOfProblems = 10;

    public PlayerController player; // player object

    public MathProblems mp;

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
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(true);
    }
    // called if the remaining time on a problem reaches 0
    void Lose()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
    }

}
