using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void CreateNewGame(string sceneName)
    {
        Cursor.visible = true;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
