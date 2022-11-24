using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour

{
    public string levelSelect = "LevelSelect";

    public string levelToLoad = "Level01";

    public void Play()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1f;
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

