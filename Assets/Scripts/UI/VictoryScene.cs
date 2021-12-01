using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour
{
    [SerializeField]
    private Button m_goToMainMenu;
    [SerializeField]
    private Button m_devTeamTrigger;
    [SerializeField]
    private AudioClip m_buttonPressClip;
    private CanvasRenderer m_devNames;

    public void Start()
    {
        m_devNames = GameObject.Find("DevNames").GetComponent<CanvasRenderer>();
        m_devNames.SetAlpha(0f);    
    }

    public void OnClickMainMenu()
    {
        AudioSource.PlayClipAtPoint(m_buttonPressClip, transform.position);
        GameManager.Instance.ExitToMenu();
    }

    public void onClickDevTeamTrigger()
    {
        
        AudioSource.PlayClipAtPoint(m_buttonPressClip, transform.position);
        m_devNames.SetAlpha(1f);
        
    }
}
