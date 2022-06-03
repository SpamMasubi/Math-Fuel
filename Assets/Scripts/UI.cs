using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    /// <summary>
    /// Tutorial Article for the template of this code: https://gamedevacademy.org/educational-games-math-tutorial/ 
    /// </summary>
    public Text equationText; //equation text
    public Text[] answers; //array of answers

    public Image fuelBar;         // fuel bar image
    private float fuelBarRate;    // 1.0 / time per problem

    public Text endText; //text displayed a the end of the game (win or game over)
    public Text correctnessText; //text displayed when correctness
    public Text problemCount; //display the total amount of problems
    public GameObject addStage, subtractStage, multiplyStage;

    public GameObject confettis;

    string operatorText = "";

    // instance
    public static UI instance;
    public static bool startGame;

    AudioSource playSFX;
    public AudioClip confettifx;

    void Awake()
    {
        // set instance to be this script
        instance = this;
    }

    void Start()
    {
        startGame = true;
        // set the fuel bar rate used to convert the time per problem (8 secs for example) and converts that to be used on a 0.0 - 1.0 scale
        fuelBarRate = 1.0f / GameManager.instance.maxFuel;
        displayAnswers(FindObjectOfType<MathProblems>());
        playSFX = GetComponent<AudioSource>();
        stageBackground();
    }

    void Update()
    {
        equationUI(FindObjectOfType<MathProblems>());
        fuelBar.fillAmount = fuelBarRate * GameManager.instance.remainingFuel;
    }

    public void equationUI(MathProblems operation)
    {
        //assign the operation symbols
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
            case MathsOperation.Division:
                operatorText = " % ";
                break;
        }
        //display the equation and the total number of problems
        equationText.text = operation.firstNumber + operatorText + operation.secondNumber + " = ?";
        problemCount.text = "Problems: " + GameManager.instance.numberOfProblems.ToString("00");
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

    void stageBackground()
    {
        switch (MainMenu.difficultRange)
        {
            case 5:
                addStage.SetActive(true);
                break;
            case 10:
                subtractStage.SetActive(true);
                break;
            case 20:
                multiplyStage.SetActive(true);
                break;
            default:
                break;
        }
    }

    void createConfetti() //creating confetties 
    {
        GameObject tempConfettis = Instantiate(confettis, correctnessText.gameObject.transform.position, Quaternion.identity);
        Destroy(tempConfettis, 2f);
    }
    
    // the text to display if the player got a correct answer or incorrect
    public IEnumerator AnswerCorrectness(bool win)
    {
        if (!GameManager.isWinOrLose)
        {
            // enable the end text object
            correctnessText.gameObject.SetActive(true);
            // did the player win?
            if (win)
            {
                createConfetti();
                playSFX.PlayOneShot(confettifx);
                correctnessText.text = "Good Job!" + "\n\n" + "This is correct!";
                correctnessText.color = Color.green;
            }
            // did the player lose?
            else
            {
                correctnessText.text = "Uh oh!" + "\n\n" + "Try Again!";
                correctnessText.color = Color.red;
            }
            yield return new WaitForSeconds(1.5f);
            // disable the end text object
            correctnessText.gameObject.SetActive(false);
        }
    }

    public void displayAnswers(MathProblems operation)
    {
        //Shout out to my good buddy and VGDA Alumni, Josh Shucker, for helping me with the solution
        //Convert correct answer to int
        int correctAnswer = int.Parse(operation.correctAnswer.ToString());

        // Create an array that contains all possible answers without the correct answer
        int numAnswers = 0;
        if(operation.curOperation == MathsOperation.Addition)
        {
            numAnswers = MainMenu.difficultRange * 2;
        }
        else if(operation.curOperation == MathsOperation.Subtraction || operation.curOperation == MathsOperation.Division)
        {
            numAnswers = MainMenu.difficultRange;
        }
        else
        {
            numAnswers = ((int)Math.Pow(MainMenu.difficultRange, 2) + 1);
        }
        //int numAnswers = ((MainMenu.difficultRange) ^ 2 + 1);
        // 0-max number base on difficulty mode (Easy: 5, Medium: 10, Hard:  20)
        //(Easy: 6, Medium = 11, Hard: 21) are the indices, so this will be one less because the correct answer is missing
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
