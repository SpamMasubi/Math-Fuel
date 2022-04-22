using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menus;
    public static string operationsType;
    public static int difficultRange;

    // Start is called before the first frame update
    void Start()
    {
        operationsType = "No operation";
        difficultRange = 0;
        Debug.Log(operationsType);
    }

    public void EasyMode()
    {
        //Easy gameplay
        difficultRange = 10;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void MediumMode()
    {
        //Medium gameplay
        difficultRange = 20;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void HardMode()
    {
        //Hard gameplay
        difficultRange = 30;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void Addition()
    {
        //solve math problems with additions
        operationsType = "Addition";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    public void Subtraction()
    {
        //solve math problems with subtractions
        operationsType = "Subtraction";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    public void Multiplication()
    {
        //solve math problems with multiplication
        operationsType = "Multiplication";
        Debug.Log(operationsType);
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
