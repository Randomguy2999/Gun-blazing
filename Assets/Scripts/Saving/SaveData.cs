using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int level;
    public float[] position;


    public SaveData(Transform playerTransform, int currentLevel)
    {
        level = currentLevel;

        position = new float[] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
    }

    public void LoadData (ref Transform playerTransform, out int currentLevel)
    {
        currentLevel = level;
        playerTransform.position = new Vector3(position[0], position[1], position[2]);
    }
}
