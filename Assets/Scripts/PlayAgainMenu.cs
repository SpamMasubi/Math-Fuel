using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainMenu : MonoBehaviour
{
    public GameObject menu;
    public Button retry, quit;

    public void playAgainProblem(MathProblems mp)
    {
        GameManager.instance.numberOfProblems = 10;
        FindObjectOfType<MathProblems>().newProblems();
        UI.instance.displayAnswers(mp);
    }

    public void Retry()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Gameplay");
        playAgainProblem(FindObjectOfType<MathProblems>());
    }

    public void Quit()
    {
        Destroy(GameManager.instance.gameObject);
        menu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }
}
