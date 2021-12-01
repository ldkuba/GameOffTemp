using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField]
    private List<Coin> m_coins;

    public CoinPickupEvent coinCollectedEvent;

    void Start()
    {
        // Add saving listener
        GameManager.Instance.saveProgressEvent.AddListener(SaveCollectiblesProgress);

        // Check that declared total number of coins in level descriptor matches the amount of coins in the level
        if(GameManager.Instance.PlayerProgress.CurrentLevel.totalCoins != m_coins.Count)
        {
            Debug.LogError("The total amount of coins declared in level descriptor does not match the amount of coins in the level");
        }

        // Set collectibles status from player save data
        bool collected = false;
        for(int i = 0; i < m_coins.Count; i++)
        {
            collected = GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].collectedCoins.Contains(i);
            m_coins[i].Init(i, collected, OnCoinPickup);
        }
    }

    private void OnCoinPickup(int coinIndex)
    {
        m_coins[coinIndex].SetCollected(true);

        // Pick up one coin, TODO: extend callback to pass amount
        coinCollectedEvent.Invoke(1);
    }

    private void SaveCollectiblesProgress()
    {
        GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].collectedCoins.Clear();
        foreach(Coin coin in m_coins)
        {
            if(coin.IsCollected())
                GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].collectedCoins.Add(coin.GetIndex());
        }
    }

    void OnDestroy()
    {
        try {
            GameManager.Instance.saveProgressEvent.RemoveListener(SaveCollectiblesProgress);
        } catch(Exception){}
    }
}
