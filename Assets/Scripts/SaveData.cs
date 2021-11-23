using System.Collections;
using System.Collections.Generic;
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

public class SaveData
{
    public LevelDescriptor CurrentLevel;
    public Dictionary<LevelDescriptor, LevelData> levelData;

    public SaveData()
    {
        levelData = new Dictionary<LevelDescriptor, LevelData>();
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
