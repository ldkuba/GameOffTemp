using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour
{
    [SerializeField]
    private Button m_goToMainMenu;
    
    public void OnClickMainMenu()
    {
        GameManager.Instance.ExitToMenu();
    }
}
