using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private int m_coinsCollected;

    void Start()
    {
        //TODO: read from player save and set initial value
        m_coinsCollected = 0;

        m_coinsCollected = GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].collectedCoins.Count;

        SetText();
    }

    public void AddCoins(int count)
    {
        Debug.Log("Adding coin");

        m_coinsCollected += count;
        SetText();
    }

    private void SetText()
    {
        GetComponent<Text>().text = m_coinsCollected + "/" + GameManager.Instance.CurrentLevel.totalCoins;
    }
}
