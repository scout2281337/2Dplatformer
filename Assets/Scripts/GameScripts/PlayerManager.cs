using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player { get; private set; }
    public PlayerStats playerStats { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SetManager();
    }

    private void SetManager()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PlayerManager could not find Player");
            return;
        }

        playerStats = player.GetComponent<PlayerStats>();
    }

    public void AddXpToPlayer(int xp)
    {
        playerStats.GainXP(xp);
    }

}
