using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private int _level;

    public void SaveGame()
    {
        SaveData data = new SaveData(_playerTransform, _level);
        BinarySave.SaveData(data);
    }
    public void LoadGame()
    {
        SaveData data = BinarySave.LoadData();
        data.LoadData(ref _playerTransform, out _level);
    }
}
