using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            PauseMenu.SetActive(isPaused);
        }
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public static void UnPause()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
