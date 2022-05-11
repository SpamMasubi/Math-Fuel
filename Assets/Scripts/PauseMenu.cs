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

    AudioSource playSFX;
    public AudioClip selectfx;

    private void Start()
    {
        playSFX = GetComponent<AudioSource>();
    }

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
        if (!isPause && !GameManager.isWinOrLose)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            isPause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            isPause = false;
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
        UI.startGame = false;
        isPause = false;
    }

    public void PlaySFX()
    {
        playSFX.PlayOneShot(selectfx);
    }

}
