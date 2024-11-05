using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    private string filePath;

    public SaveSystem()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    }
    public void SaveData(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath, json);

        Debug.Log(filePath);
    }

    public GameData LoadData()
    {
        if (!IsFileExist()) return null;

        string json = File.ReadAllText(filePath);

        GameData gameData = JsonUtility.FromJson<GameData>(json);

        return gameData;
    }
    public bool IsFileExist()
    {
        return File.Exists(filePath);
    }

    public void DeleteSaveFile() => File.Delete(filePath);
}
