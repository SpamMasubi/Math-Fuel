using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button pause, continueButton, quit;

    public GameObject pauseMenu;

    public static bool isPause;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (isPause)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            isPause = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            isPause = true;
        }
    }

    public void continueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPause = false;
    }

    public void Quit()
    {
        Destroy(GameManager.instance.gameObject);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }


}
