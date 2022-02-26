using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Game is started!");
    }

    public void QuitGame()
    {
        Debug.Log("Game is closed!");
        Application.Quit();
    }
}