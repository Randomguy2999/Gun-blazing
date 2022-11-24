using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string mainMenu = "MainMenu";

    public Button[] levelButtons;


    public void Select(string level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}