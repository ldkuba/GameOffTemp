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

        GameManager.Instance.coinCollectedEvent.AddListener(AddCoins);
        SetText();
    }

    public void AddCoins(int count)
    {
        m_coinsCollected += count;
        SetText();
    }

    private void SetText()
    {
        GetComponent<Text>().text = m_coinsCollected + "/" + GameManager.Instance.CurrentLevel.totalCoins;
    }

    void OnDestroy()
    {
        try {
            GameManager.Instance.coinCollectedEvent.RemoveListener(AddCoins);
        } catch(Exception){}
    }
}
