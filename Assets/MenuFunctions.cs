using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void startGame()
    {
        GameResults.Instance.lastLevel = 0;
        SceneManager.LoadScene(GameResults.Instance.getLevelName());
    }

    public void showCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void endGame()
    {
        if (GameResults.Instance && GameResults.Instance.didWin)
        {
            mainMenu();
        }
        else
        {
            SceneManager.LoadScene(GameResults.Instance.lastLevel);
        }        
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
