using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : AIController
{
    private bool m_isMovingRight;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_lethalDistance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_isMovingRight = (Random.value > 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.FindGameObjectsWithTag

        if (m_isMovingRight)
        {
            m_horizontalMove = m_speed;
        }else
        {
            m_horizontalMove = -m_speed;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("VertObstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 avgNormal = new Vector2(0, 0);

            foreach (ContactPoint2D point in collision.contacts)
            {
                avgNormal.x += point.normal.x;
                avgNormal.y += point.normal.y;
            }

            avgNormal /= collision.contactCount;

            if (Mathf.Abs(avgNormal.x) > Mathf.Abs(avgNormal.y))
            {
                //Debug.Log("Changing direction, normal: " + avgNormal);
                m_isMovingRight = !m_isMovingRight;
            }
        }
    }

    public override void EdgeTriggerEvent(EdgeTrigger edge)
    {
        if(m_isMovingRight)
        {
            if (edge.jumpRight)
            {
                m_jump = true;
            }
            else if (edge.fallRight) { /*noop*/}
            else
            {
                m_isMovingRight = !m_isMovingRight;
            }
        }else
        {
            if (edge.jumpLeft)
            {
                m_jump = true;
            }
            else if (edge.fallLeft) { /*noop*/}
            else
            {
                m_isMovingRight = !m_isMovingRight;
            }
        }
    }
}
