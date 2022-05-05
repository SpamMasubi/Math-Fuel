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

    float TimerForNextUseGas, Cooldown;

    public float maxFuel;    // time allowed to answer each problem
    public float remainingFuel;     // time remaining for the current problem

    public PlayerController player; // player object

    public MathProblems mp;

    public static bool isWinOrLose;

    void Awake()
    {
        // set instance to this script.
        instance = this;
    }

    void Start()
    {
        Cooldown = 3f;
        TimerForNextUseGas = Cooldown;
        player = FindObjectOfType<PlayerController>();
        mp = FindObjectOfType<MathProblems>();
        remainingFuel = maxFuel;
    }

    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        else
        {
            if (UI.startGame)
            {
                usingGasUp();
                // has the remaining time ran out?
                if (remainingFuel <= 0.0f)
                {
                    Lose();
                }
            }
        }
    }

    // called when the player enters the correct tube
    void CorrectAnswer()
    {
        numberOfProblems -= 1;
        remainingFuel += 3.0f;
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

    public void resetFuelBar()
    {
        remainingFuel = maxFuel;
    }

    void usingGasUp()
    {
        if (TimerForNextUseGas > 0)
        {
            TimerForNextUseGas -= Time.deltaTime;
        }
        else if (TimerForNextUseGas <= 0)
        {
            remainingFuel -= 3.0f;
            TimerForNextUseGas = Cooldown;
        }
    }

    public void OnPlayerEnterFuelStation(int fuelStation)
    {
        if (UI.instance.answers[fuelStation].text == mp.correctAnswer.ToString()){
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
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
