using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject DeathPanel;
    public int MaxHealth = 100;
    [SerializeField] private int Currenthealth;
    public Healthbar Healthbar;

    private PlayerMovement playerMovement;

    // Для неуязвимости
    public float invincibilityDuration = 1.0f; 
    private bool isInvincible = false; 
    void Start()
    {
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        
        if (isInvincible)
            return;

        // Наносим урон
        Currenthealth -= damage;
        Healthbar.SetHealth(Currenthealth);

        
        StartCoroutine(Invincibility());

        if (Currenthealth <= 0)
        {
            StartCoroutine(RestartLevel()); 
        }
    }

    
    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration); 
        isInvincible = false; 
    }

    IEnumerator RestartLevel()
    {
        playerMovement.enabled = false;
        DeathPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
