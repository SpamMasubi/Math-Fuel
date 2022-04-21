using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathProblems : MonoBehaviour
{
    public int firstNumber;
    public int secondNumber;
    public int correctAnswer;

    [HideInInspector]public MathsOperation curOperation;
    // Start is called before the first frame update
    void Start()
    {
        firstNumber = Random.Range(0, 10);
        secondNumber = Random.Range(0, 10);
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
            default:
                break;
        }

        return curOperation;
    }

    public void newProblems()
    {
        firstNumber = Random.Range(0, 10);
        secondNumber = Random.Range(0, 10);
        operation(MainMenu.operationsType);
    }

}

public enum MathsOperation
{
    Addition,
    Subtraction,
    Multiplication,
}
