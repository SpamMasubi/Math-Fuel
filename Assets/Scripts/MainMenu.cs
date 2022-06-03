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

    AudioSource playSFX;
    public AudioClip selectfx;

    // Start is called before the first frame update
    void Start()
    {
        operationsType = "No operation";
        difficultRange = 0;
        playSFX = GetComponent<AudioSource>();
    }

    public void EasyMode()
    {
        //Easy gameplay
        difficultRange = 5;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void MediumMode()
    {
        //Medium gameplay
        difficultRange = 10;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void HardMode()
    {
        //Hard gameplay
        difficultRange = 20;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    public void Addition()
    {
        //solve math problems with additions
        operationsType = "Addition";
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    public void Subtraction()
    {
        //solve math problems with subtractions
        operationsType = "Subtraction";
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    public void Multiplication()
    {
        //solve math problems with multiplication
        operationsType = "Multiplication";
        FindObjectOfType<MathProblems>().operation(operationsType);
        Invoke("LoadGame", 1); //Load the game
    }

    public void Division()
    {
        //solve math problems with Division
        operationsType = "Division";
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

    public void PlaySFX()
    {
        playSFX.PlayOneShot(selectfx);
    }
}
