using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    Debug.LogError("GameManager is missing!");
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

    // ================== SCENE SWITCHING ========================

    public void Play(LevelDescriptor level = null)
    {
        if(PlayerProgress == null)
        {
            PlayerProgress = new SaveData();
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

        StartCoroutine(LoadScene(CurrentLevel.scenePath));
    }

    public void LevelSelect()
    {
        StartCoroutine(LoadScene(LevelsScene));
    }

    public void ExitToMenu()
    {
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
