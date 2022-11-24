using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    


    private void OnEnable()
    {
        PauseManager.Pause();
        if (GameObject.Find("Main Camera") != null) 
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false; // Stops the camera from following the players mouse movement
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        PauseManager.UnPause();
        if (GameObject.Find("Main Camera") != null) 
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
