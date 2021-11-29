using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FakeSwitch : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_audioClip;

    private bool m_attackFinished = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerHit")
        {
            m_attackFinished = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerHit")
        {
            PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>() as PlayerController;
            
            Debug.Log(player.IsHitting);
            if(player.IsHitting)
            {
                AudioSource audioSource = GetComponent<AudioSource>() as AudioSource;
                if(m_attackFinished)
                {
                    if(audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }

                    audioSource.PlayOneShot(m_audioClip);
                    m_attackFinished = false;
                }
            }else
            {
                m_attackFinished = true;
            }
        }     
    }
}
