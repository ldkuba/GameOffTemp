using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

// args: amount
[System.Serializable]
public class CoinCollectedEvent : UnityEvent<int> {}

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

        // TODO: load from save file if file exists
        PlayerProgress = new SaveData();

        coinCollectedEvent = new CoinCollectedEvent();
        saveProgressEvent = new SaveProgressEvent();

        Application.targetFrameRate = 60;
    }

    void Update()
    {
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

    // Stores the players progess
    public SaveData PlayerProgress { get; private set; }
    public SaveProgressEvent saveProgressEvent;

    // ================== SCENE SWITCHING ========================

    public void Play(LevelDescriptor level = null)
    {
        if(CurrentLevel != null)
        {
            saveProgressEvent.Invoke();
        }

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
        if(CurrentLevel != null)
        {
            saveProgressEvent.Invoke();
        }

        CurrentLevel = null;

        StartCoroutine(LoadScene(LevelsScene));
    }

    public void ExitToMenu()
    {
        if(CurrentLevel != null)
        {
            saveProgressEvent.Invoke();
        }

        CurrentLevel = null;

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

    // ========================== COINS, COLLECTABLES AND SCORE ===============================

    public CoinCollectedEvent coinCollectedEvent;

}
