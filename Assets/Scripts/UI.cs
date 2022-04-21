using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    public Text equationText; //equation text
    public Text[] answers; //array of answers
    public Text endText; //text displayed a the end of the game (win or game over)

    string operatorText = "";

    // instance
    public static UI instance;
    void Awake()
    {
        // set instance to be this script
        instance = this;
    }

    void Start()
    {
        displayAnswers(FindObjectOfType<MathProblems>());
    }

    void Update()
    {
        equationUI(FindObjectOfType<MathProblems>());
    }

    public void equationUI(MathProblems operation)
    {
        switch (operation.curOperation)
        {
            case MathsOperation.Addition:
                operatorText = " + ";
                break;
            case MathsOperation.Subtraction:
                operatorText = " - ";
                break;
            case MathsOperation.Multiplication:
                operatorText = " x ";
                break;
        }
        equationText.text = operation.firstNumber + operatorText + operation.secondNumber;
    }

    // sets the end text to display if the player won or lost
    public void SetEndText(bool win)
    {
        // enable the end text object
        endText.gameObject.SetActive(true);
        // did the player win?
        if (win)
        {
            endText.text = "You Win!";
            endText.color = Color.green;
        }
        // did the player lose?
        else
        {
            endText.text = "Game Over!";
            endText.color = Color.red;
        }
        GetComponent<PlayAgainMenu>().menu.SetActive(true);
    }

    public void displayAnswers(MathProblems operation)
    {
        //Shout out to my good buddy and VGDA Alumni, Josh Shucker, for helping me with the solution
        //Convert correct answer to int
        int correctAnswer = int.Parse(operation.correctAnswer.ToString());

        // Create an array that contains all possible answers without the correct answer
        int numAnswers = 20;
        // 0-20 is 21 indices, so this will be one less because the correct answer is missing
        int[] answerArray = new int[numAnswers];
        for (int i = 0; i < numAnswers; i++)
        {
            if (i >= correctAnswer)
            {
                answerArray[i] = i + 1;
            }
            else
            {
                answerArray[i] = i;
            }
        }

        //Now we have an array of all numbers that are not the correct answer

        //Shuffle the wrong answer array
        for (int i = 0; i < answerArray.Length; i++)
        {
            int rnd = Random.Range(0, answerArray.Length);
            int cachedValue = answerArray[rnd];
            answerArray[rnd] = answerArray[i];
            answerArray[i] = cachedValue;
        }

        //Overwrite one of the wrong answers in the array with the correct one
        int correctAnswerIndex = Random.Range(0, 4);
        answerArray[correctAnswerIndex] = correctAnswer;

        //Fill out info in answer UI text
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].text = answerArray[i].ToString();
        }
    }
}