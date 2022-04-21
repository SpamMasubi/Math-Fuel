using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button[] operationButtons;
    public static string operationsType;

    // Start is called before the first frame update
    void Start()
    {
        operationsType = "No operation";
        Debug.Log(operationsType);
    }

    public void Addition()
    {
        operationsType = "Addition";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1);
    }

    public void Subtraction()
    {
        operationsType = "Subtraction";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1);
    }

    public void Multiplication()
    {
        operationsType = "Multiplication";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1);
    }

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
