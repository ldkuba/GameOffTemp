using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
// args: amount
public class CoinPickupEvent : UnityEvent<int> {}

public class Coin : MonoBehaviour
{
    private Action<int> m_coinPickupCallback;

    [SerializeField]
    private int rotateSpeed;

    private int m_index;
    public int GetIndex() { return m_index; }
    private bool m_collected;
    public bool IsCollected() {return m_collected;}

    public void Init(int index, bool collected, Action<int> pickupCallback)
    {
        m_index = index;
        SetCollected(collected);

        m_coinPickupCallback = pickupCallback;
    }

    public void SetCollected(bool collected)
    {
        m_collected = collected;

        GetComponent<SpriteRenderer>().enabled = !collected;
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_collected) 
            return;

        if(collision.gameObject.tag == "Player")
        {
            m_coinPickupCallback(m_index);
        }     
    }
}
