using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Note this is not a MonoBehaviour
/// TODO: overwriting check, error handling, incremental read.
/// TODO2: encryption
/// </summary>
public class SaveFile
{
    public string fileName = "save_game.json";
    public string saveFile;

    public SaveFile()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }
    public void Save(MonoBehaviour data)
    {
        string textData = JsonUtility.ToJson(data);
        File.WriteAllText(saveFile, textData);
    }

    public MonoBehaviour Load()
    {
        MonoBehaviour data = null;
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(fileName);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            data = JsonUtility.FromJson<MonoBehaviour>(fileContents);
        }
        return data;
    }
    
}
