using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realization : MonoBehaviour
{
    private bool m_oneShotFired;

    void Start()
    {
        m_oneShotFired = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!m_oneShotFired)
            {
                GetComponent<AudioSource>().Play();
                m_oneShotFired = true;
            }
        }
    }
}
