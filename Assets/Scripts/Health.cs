using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject DeathPanel;
    public int MaxHealth = 100;
    [SerializeField] private int Currenthealth;

    public Healthbar Healthbar;


    private PlayerMovement playerMovement;
    void Start()
    {
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        Currenthealth -= damage;
        Healthbar.SetHealth(Currenthealth);

        if (Currenthealth <= 0)
        {
            StartCoroutine(RestartLevel()); // Вызов корутины
        }
    }

    IEnumerator RestartLevel()
    {
        playerMovement.enabled = false;
        DeathPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
