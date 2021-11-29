using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SwitchEvent : UnityEvent<bool> {}

public class Switch : MonoBehaviour
{
    public SwitchEvent switchEvent;

    private bool m_active = false;

    private Quaternion m_lockedState;
    private Quaternion m_unlockedState;

    [SerializeField]
    private float m_speed = 100.0f;

    private bool m_isTurning = false;

    [SerializeField]
    private Color m_lockedColor;
    [SerializeField, ColorUsage(true, true)]
    private Color m_lockedColorOutline;
    [SerializeField]
    private Color m_unlockedColor;
    [SerializeField, ColorUsage(true, true)]
    private Color m_unlockedColorOutline;

    private void SetColor()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", m_active ? m_unlockedColor : m_lockedColor);
        GetComponent<SpriteRenderer>().material.SetColor("_SolidOutline", m_active ? m_unlockedColorOutline : m_lockedColorOutline);
    }

    void Start()
    {
        m_lockedState = Quaternion.identity;
        m_unlockedState = Quaternion.Euler(0, 0, 90.0f);

        SetColor();
    }

    public void TriggerSwitch()
    {
        m_active = !m_active;
        switchEvent.Invoke(m_active);

        SetColor();

        m_isTurning = true;
    }

    void Update()
    {
        float step = m_speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, (m_active ? m_unlockedState : m_lockedState), step);

        if(m_isTurning)
        {
            if(transform.rotation == m_lockedState || transform.rotation == m_unlockedState)
            {
                m_isTurning = false;                    
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerHit")
        {
            if(!m_isTurning)
            {
                PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>() as PlayerController;
                if(player.IsHitting)
                {
                    TriggerSwitch();
                }
            }
        }     
    }
}
