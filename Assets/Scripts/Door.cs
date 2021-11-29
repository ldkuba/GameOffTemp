using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform m_closedPos;

    [SerializeField]
    private Transform m_openPos;

    [SerializeField]
    private GameObject m_door;

    private bool m_open;

    [SerializeField]
    private float m_speed = 5.0f;

    public void SetState(bool open)
    {
        m_open = open;
    }

    public void Toggle()
    {
        m_open = !m_open;
    }

    void Update()
    {
        float step = m_speed * Time.deltaTime;
        m_door.transform.position = Vector3.MoveTowards(m_door.transform.position, (m_open ? m_openPos.position : m_closedPos.position), step);
    }
}
