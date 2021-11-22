using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button m_playButton;

    [SerializeField]
    private Button m_levelsButton;
    
    [SerializeField]
    private Button m_quitButton;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.PlayerProgress != null)
        {
            m_playButton.GetComponentInChildren<Text>().text = "CONTINUE";
        }else
        {
            m_playButton.GetComponentInChildren<Text>().text = "PLAY";
        }
    }

    public void OnClickPlay()
    {
        GameManager.Instance.Play();
    }

    public void OnClickLevels()
    {

    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
