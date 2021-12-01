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

    [SerializeField]
    private AudioClip m_pressbuttonclip;

    // Start is called before the first frame update
    void Start()
    {
        Repaint();
    }

    private void Repaint()
    {
        if(GameManager.Instance.PlayerProgress.CurrentLevel != null)
        {
            m_playButton.GetComponentInChildren<Text>().text = "CONTINUE";
        }else
        {
            m_playButton.GetComponentInChildren<Text>().text = "PLAY";
        }
    }

    public void OnClickPlay()
    {
        AudioSource.PlayClipAtPoint(m_pressbuttonclip, transform.position);
        GameManager.Instance.Play();
    }

    public void OnClickLevels()
    {
        AudioSource.PlayClipAtPoint(m_pressbuttonclip, transform.position);
        GameManager.Instance.LevelSelect();
    }

    public void OnClickResetSave()
    {
        AudioSource.PlayClipAtPoint(m_pressbuttonclip, transform.position);
        GameManager.Instance.ResetSaveData();

        Repaint();
    }

    public void OnClickQuit()
    {
        AudioSource.PlayClipAtPoint(m_pressbuttonclip, transform.position);
        Application.Quit();
    }
}
