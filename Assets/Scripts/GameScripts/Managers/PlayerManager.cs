using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player { get; private set; }
    public PlayerMovement playerMovement {  get; private set; }
    public PlayerCombat playerCombat { get; private set; }
    public PlayerHealth playerHealth { get; private set; }
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

        playerMovement = player.GetComponent<PlayerMovement>();
        playerCombat = player.GetComponent<PlayerCombat>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void AddXpToPlayer(int xp)
    {
        playerStats.GainXP(xp);
    }
}
