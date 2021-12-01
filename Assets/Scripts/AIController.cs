using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour
{
    protected CharacterController2D m_controller;
    protected float m_horizontalMove = 0.0f;
    protected bool m_jump = false;
    protected bool m_crouch = false; // not used currently

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_controller = GetComponent<CharacterController2D>();
    }

    public abstract void EdgeTriggerEvent(EdgeTrigger edge);

    protected virtual void FixedUpdate()
    {
        m_controller.Move(m_horizontalMove * Time.fixedDeltaTime, m_crouch, m_jump);
        m_jump = false;
    }
}
