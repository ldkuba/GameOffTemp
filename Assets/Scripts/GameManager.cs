using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

[System.Serializable]
public class SaveProgressEvent : UnityEvent {}

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance
    {
        get {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<GameManager>();
                
                if (m_instance == null)
                {
                    throw new System.Exception("GameManager is missing!");
                }
            }
        
            return m_instance;
        }
    }

    void Start()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        m_instance = this;

        // Load from file or create new
        PlayerProgress = SaveData.LoadSaveFile(m_saveFilename);

        saveProgressEvent = new SaveProgressEvent();

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // // TODO: catch exception on NullPointers so that 0 levels played doesnt mean no save data
        // try{
        //     string saveData = PlayerProgress.ToString();
        //     Debug.Log(saveData);
        // }catch(Exception)
        // {
        //     Debug.Log("No save data available");
        // }
    }

    // ========================================================================

    [SerializeField]
    private List<LevelDescriptor> m_levels;
    public List<LevelDescriptor> Levels { get { return m_levels; } }
    public LevelDescriptor CurrentLevel { get; private set; }

    [SerializeField]
    private string m_menuScene;
    public string MenuScene { get { return m_menuScene; } }

    [SerializeField]
    private string m_levelsScene;
    public string LevelsScene { get { return m_levelsScene; } }

    [SerializeField] 
    private string m_victoryScene;
    public string VictoryScene { get { return m_victoryScene; } }

    // ================== SAVE DATA ================================

    // Stores the players progess
    public SaveData PlayerProgress { get; private set; }
    public SaveProgressEvent saveProgressEvent;

    // Currently only supports one player profile
    private const string m_saveFilename = "savegame.trololo";

    private void SavePlayerProgress()
    {
        if(CurrentLevel != null)
        {
            saveProgressEvent.Invoke();
            PlayerProgress.SaveToFile(m_saveFilename);
        }
    }

    public void ResetSaveData()
    {
        PlayerProgress = new SaveData();
        PlayerProgress.SaveToFile(m_saveFilename);
    }

    // ================== SCENE SWITCHING ========================

    public void PlayNext()
    {
        if(CurrentLevel == null)
        {
            Play();
        }else
        {
            int nextLevelIndex = m_levels.IndexOf(CurrentLevel) + 1;
            if(nextLevelIndex >= m_levels.Count)
            {
                // Congratulations you won!
                // TODO: some outro scene
                SavePlayerProgress();
                CurrentLevel = null;
                
                StartCoroutine(LoadScene(VictoryScene));
                // ExitToMenu();
                return;
            }

            Play(m_levels[nextLevelIndex]);
        }
    }

    public void Play(LevelDescriptor level = null)
    {
        SavePlayerProgress();
        GetComponent<GameManager>().GetComponent<AudioSource>().Stop();
        if(level == null)
        {
            if(PlayerProgress.CurrentLevel == null)
            {
                if(m_levels.Count == 0)
                {
                    Debug.LogError("There are no registered levels");
                    return;
                }

                CurrentLevel = m_levels[0];
            }else
            {
                CurrentLevel = PlayerProgress.CurrentLevel;
            }
        }else
        {
            if(!m_levels.Contains(level))
            {
                Debug.LogError("The level: " + level.levelName + " is not registered");
                return;
            }
            
            CurrentLevel = level;
        }

        // Set player progress to target level
        PlayerProgress.CurrentLevel = CurrentLevel;
        if(!PlayerProgress.levelData.ContainsKey(CurrentLevel))
        {
            PlayerProgress.levelData.Add(CurrentLevel, new LevelData());
        }

        StartCoroutine(LoadScene(CurrentLevel.scenePath));
    }

    public void LevelSelect()
    {
        SavePlayerProgress();

        CurrentLevel = null;

        StartCoroutine(LoadScene(LevelsScene));
    }

    public void ExitToMenu()
    {
        SavePlayerProgress();

        CurrentLevel = null;
        GetComponent<GameManager>().GetComponent<AudioSource>().Play();
        StartCoroutine(LoadScene(MenuScene));
    }

    IEnumerator LoadScene(string scenePath)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
