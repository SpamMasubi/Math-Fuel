using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathProblems : MonoBehaviour
{
    public int firstNumber;
    public int secondNumber;
    public int correctAnswer;

    private int maxRange;

    [HideInInspector]public MathsOperation curOperation;
    // Start is called before the first frame update
    void Start()
    {
        firstNumber = Random.Range(0, 6);
        secondNumber = Random.Range(0, 6);
    }

    public void numberRangeRandomizer()
    {
        switch (MainMenu.difficultRange)
        {
            case 5:
                maxRange = 6;
                firstNumber = Random.Range(0, maxRange);
                secondNumber = Random.Range(0, maxRange);
                break;
            case 10:
                maxRange = 11;
                firstNumber = Random.Range(0, maxRange);
                secondNumber = Random.Range(0, maxRange);
                break;
            case 20:
                maxRange = 21;
                firstNumber = Random.Range(0, maxRange);
                secondNumber = Random.Range(0, maxRange);
                break;
            default:
                break;
        }
    }

    public MathsOperation operation(string operationType)
    {
        switch (operationType)
        {
            case "Addition":
                curOperation = MathsOperation.Addition;
                correctAnswer = firstNumber + secondNumber;
                break;
            case "Subtraction":
                curOperation = MathsOperation.Subtraction;
                if(firstNumber < secondNumber)
                {
                    int temp = firstNumber;
                    firstNumber = secondNumber;
                    secondNumber = temp;
                    
                }
                correctAnswer = firstNumber - secondNumber;
                break;
            case "Multiplication":
                curOperation = MathsOperation.Multiplication;
                correctAnswer = firstNumber * secondNumber;
                break;
            case "Division":
                curOperation = MathsOperation.Division;
                if (secondNumber != 0)
                {
                    int divAns = firstNumber * secondNumber; //first multiply the first and second number
                    firstNumber = divAns; //the total answer will become the first number;
                    correctAnswer = firstNumber / secondNumber;
                }
                else
                {
                    numberRangeRandomizer();
                    operation(MainMenu.operationsType);
                }
                break;
            default:
                break;
        }

        return curOperation;
    }

    public void newProblems()
    {
        numberRangeRandomizer();
        operation(MainMenu.operationsType);
    }

}

public enum MathsOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
