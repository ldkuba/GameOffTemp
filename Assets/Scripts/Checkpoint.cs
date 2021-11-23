using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointTriggerEvent : UnityEvent<int> {}

public class Checkpoint : MonoBehaviour
{
    public CheckpointTriggerEvent checkpointTriggerEvent;

    [SerializeField, ColorUsage(true, true)]
    private Color m_disabledColor;
    [SerializeField, ColorUsage(true, true)]
    private Color m_enabledColor;


    private int m_index;
    private bool m_active;

    public void Init(int index, bool active)
    {
        m_index = index;
        m_active = active;

        checkpointTriggerEvent = new CheckpointTriggerEvent();

        SetColour(active);
    }

    public void SetActive(bool active)
    {
        m_active = active;

        SetColour(active);
    }

    private void SetColour(bool active)
    {
        GetComponent<SpriteRenderer>().material.SetColor("_SolidOutline", active ? m_enabledColor : m_disabledColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            checkpointTriggerEvent.Invoke(m_index);
        }     
    }
}
