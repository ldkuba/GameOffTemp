using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool active = false;

    [SerializeField]
    private GameObject m_root;
    [SerializeField]
    private AudioClip m_buttonPressClip;

    private void ToggleActive()
    {
        SetMenuActive(!active);
    }

    private void SetMenuActive(bool menuActive)
    {
        m_root.SetActive(active = menuActive);
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            ToggleActive();
        }
    }

    public void OnResume()
    {
        AudioSource.PlayClipAtPoint(m_buttonPressClip, new Vector3(0, 0, 0));
        SetMenuActive(false);
    }

    public void OnQuitToMenu()
    {
        AudioSource.PlayClipAtPoint(m_buttonPressClip, new Vector3(0, 0, 0));
        GameManager.Instance.ExitToMenu();
    }
}
