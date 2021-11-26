using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private List<Checkpoint> m_checkpoints;

    [SerializeField]
    private GameObject m_player;

    private int lastCheckpoint;

    private bool m_isSwitchingScene;

    void Start()
    {
        // Add saving listener
        GameManager.Instance.saveProgressEvent.AddListener(SaveCheckpointProgress);

        m_isSwitchingScene = false;

        // Set lastCheckpoint from player save data or start from 0
        lastCheckpoint = GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].lastCheckpoint;
        if(lastCheckpoint == -1)
        {
            lastCheckpoint = 0;
        }

        // Set collision callback
        for(int i = 0; i < m_checkpoints.Count; i++)
        {
            m_checkpoints[i].Init(i, i == lastCheckpoint);
            m_checkpoints[i].checkpointTriggerEvent.AddListener(TriggerCheckpoint);
        }

        // Move player to checkpoint
        MoveToCheckpoint(lastCheckpoint);
    }

    public void TriggerCheckpoint(int newCheckpoint)
    {
        if(m_isSwitchingScene)
            return;

        // Replace last checkpoint with new checkpoint if it has a larger index then the previous one
        // Not sure if thats what we want, maybe make different kinds of behavious like alwaysReplace, replaceIncremental, etc.
        if(newCheckpoint > lastCheckpoint)
        {
            // Check if this is the final checkpoint
            if(m_checkpoints[newCheckpoint].isFinalCheckpoint)
            {
                lastCheckpoint = 0;
                m_isSwitchingScene = true;
                
                GameManager.Instance.PlayNext();
                return;
            }

            m_checkpoints[lastCheckpoint].SetActive(false);
            m_checkpoints[newCheckpoint].SetActive(true);

            lastCheckpoint = newCheckpoint;
        }
    }

    private void MoveToCheckpoint(int checkpoint)
    {
        m_player.transform.position = m_checkpoints[lastCheckpoint].transform.position;
    }

    void Update()
    {
        if(Input.GetButtonDown("ResetCheckpoint"))
        {
            MoveToCheckpoint(lastCheckpoint);
        }
    }

    private void SaveCheckpointProgress()
    {
        GameManager.Instance.PlayerProgress.levelData[GameManager.Instance.CurrentLevel].lastCheckpoint = lastCheckpoint;
    }

    void OnDestroy()
    {
        try {
            GameManager.Instance.saveProgressEvent.RemoveListener(SaveCheckpointProgress);
        } catch(Exception){}
    }
}
