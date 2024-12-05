using UnityEngine;
using Zenject;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player { get; private set; }
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerInteraction playerInteraction;
    public PlayerHealth playerHealth;
    public PlayerStats playerStats; 




    [Inject] 
    public void Constructor(PlayerMovement playerMovement, PlayerCombat playerCombat, PlayerInteraction playerInteraction, PlayerHealth playerHealth, PlayerStats playerStats) 
    {
        this.playerMovement = playerMovement;
        this.playerCombat = playerCombat;
        this.playerInteraction = playerInteraction;
        this.playerHealth = playerHealth;
        this.playerStats = playerStats;

    }

    protected override void Awake()
    {
        base.Awake();

        SetManager();
    }

    
    // we dont need setmanger anymore вот уже хз залупы € понаделал
    private void SetManager()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PlayerManager could not find Player");
            return;
        }
        /*
        playerMovement = player.GetComponent<PlayerMovement>();
        playerCombat = player.GetComponent<PlayerCombat>();
        playerInteraction = player.GetComponent<PlayerInteraction>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerStats = player.GetComponent<PlayerStats>();
        */
    }

    public void AddXpToPlayer(int xp)
    {
        playerStats.GainXP(xp);
    }
}
