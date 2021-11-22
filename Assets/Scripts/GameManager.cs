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
    private List<string> m_levels;
    public List<string> Levels { get; }
    public string CurrentLevel { get; private set; }

    [SerializeField]
    private string m_menuScene;
    public string MenuScene { get; }

    [SerializeField]
    private string m_levelsScene;
    public string LevelsScene { get; }

    // Stores the players progess
    public SaveData PlayerProgress { get; private set; }

    // ================== SCENE SWITCHING ========================

    public void Play()
    {
        if(PlayerProgress == null)
        {
            // If this object is being created, the game has just been launched
            // Set current level to level 1
            if(m_levels.Count == 0)
            {
                Debug.LogError("There are no registered levels!");
                return;
            }

            // Set current level to first level
            CurrentLevel = m_levels[0];

            // Create player save data
            PlayerProgress = new SaveData();
            PlayerProgress.CurrentLevel = CurrentLevel;
        }

        StartCoroutine(LoadScene(CurrentLevel));
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
