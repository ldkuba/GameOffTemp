using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool active = false;

    [SerializeField]
    private GameObject m_root;

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
        SetMenuActive(false);
    }

    public void OnQuitToMenu()
    {
        GameManager.Instance.ExitToMenu();
    }
}
