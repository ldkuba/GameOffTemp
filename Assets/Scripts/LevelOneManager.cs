using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : LevelManager
{
    [SerializeField] private Transform m_spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        m_lives = 3;
    }

    public override void resetLevel()
    {
        m_lives--;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = m_spawnLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
