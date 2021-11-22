using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Level/LevelDescriptor", order = 1)]
public class LevelDescriptor : ScriptableObject
{
    public string scenePath;
    public string levelName;
    public Sprite thumbnail;
}
