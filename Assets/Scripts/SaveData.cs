using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    // store reference to coins as position in CoinManager list
    public List<int> collectedCoins;

    // store reference to checkpoints as reference to position in CheckpointManager
    public int lastCheckpoint = -1;

    public override string ToString()
    {
        return "Last Checkpoint: " + lastCheckpoint;
    } 
}

[System.Serializable]
public class SaveData : ISerializable
{
    public static string SaveFormatVersion = "0.1";

    // Determines the format version used for this save file
    // Save files with missmatching version strings will not be loaded by the game
    public string formatVersion;

    public LevelDescriptor CurrentLevel;
    public Dictionary<LevelDescriptor, LevelData> levelData;

    public SaveData()
    {
        levelData = new Dictionary<LevelDescriptor, LevelData>();
    }

    public void SaveToFile(string filePath)
    {
        string fullPath = Application.persistentDataPath + "/" + filePath;

        // Set save format version before writing to file
        formatVersion = SaveFormatVersion;

        FileStream dataStream = new FileStream(fullPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(dataStream, this);

        dataStream.Close();
    }

    // Returns save file object loaded from file or a new SaveData object if file was not found
    public static SaveData LoadSaveFile(string filePath)
    {
        string fullPath = Application.persistentDataPath + "/" + filePath;

        SaveData ret = new SaveData();

        try {
            FileStream dataStream = new FileStream(fullPath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            ret = formatter.Deserialize(dataStream) as SaveData;

            if(ret.formatVersion != SaveData.SaveFormatVersion)
            {
                ret = new SaveData();
            }

            dataStream.Close();
        }catch(FileNotFoundException) 
        {
            Debug.Log("Creating new save file");
        }catch(System.Exception)
        {
            Debug.Log("Corrupted save file");
        }

        return ret;
    }

    protected SaveData(SerializationInfo info, StreamingContext context)
    {
        // formatVersion
        formatVersion = info.GetValue("formatVersion", typeof(string)) as string;

        //CurrentLevel
        string currentLevelString = info.GetValue("currentLevel", typeof(string)) as string;
        if(currentLevelString == "")
        {
            CurrentLevel = null;
        }else
        {
            CurrentLevel = GameManager.Instance.Levels.Find(x => x.scenePath == currentLevelString);
            if(CurrentLevel == null)
            {
                throw new System.Exception("Could not find CurrentLevel during deserialization");
            }
        }

        // levelData
        levelData = new Dictionary<LevelDescriptor, LevelData>();
        var deserializedLevelData = info.GetValue("levelData", typeof(List<System.Tuple<string, LevelData>>)) as List<System.Tuple<string, LevelData>>;
        foreach(var entry in deserializedLevelData)
        {
            LevelDescriptor levelDescriptor = GameManager.Instance.Levels.Find(x => x.scenePath == entry.Item1);
            if(levelDescriptor == null)
            {
                throw new System.Exception("Could not find LevelDescriptor for: " + entry.Item1 + " during deserialization");
            }
            levelData.Add(levelDescriptor, entry.Item2);
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        // formatVersion
        info.AddValue("formatVersion", formatVersion, typeof(string));

        // CurrentLevel
        if(CurrentLevel != null)
        {
            info.AddValue("currentLevel", CurrentLevel.scenePath, typeof(string));
        }else
        {
            info.AddValue("currentLevel", "", typeof(string));
        }

        // levelData
        var serializableLevelData = new List<System.Tuple<string, LevelData>>();
        foreach(var entry in levelData)
        {
            serializableLevelData.Add(new System.Tuple<string, LevelData>(entry.Key.scenePath, entry.Value));
        }
        info.AddValue("levelData", serializableLevelData, typeof(List<System.Tuple<string, LevelData>>));
    }

    public override string ToString()
    {
        string ret = "Current Level: " + CurrentLevel.levelName + " |";

        foreach(LevelData level in levelData.Values)
        {
            ret += " " + level.ToString();
        }

        return ret;
    }
}
