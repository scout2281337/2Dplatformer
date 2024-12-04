using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float regenerationAmount = 1;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject deathPanel;
    private PlayerMovement playerMovement;

    // Для неуязвимости
    [SerializeField] private float invincibilityDuration = 1.0f;
    private bool isInvincible = false;

    // Переменная для отслеживания корутины регенерации
    private Coroutine healingCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMovement = GetComponent<PlayerMovement>();
        healingCoroutine = StartCoroutine(Healing());
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
            return;

        // Наносим урон
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Останавливаем корутину регенерации, если получаем урон
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }

        // Запускаем корутину неуязвимости
        StartCoroutine(Invincibility());

        // Проверка на смерть
        if (currentHealth <= 0)
        {
            StartCoroutine(RestartLevel());
        }
        else
        {
            // Перезапускаем регенерацию
            healingCoroutine = StartCoroutine(Healing());
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
        deathPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Healing()
    {
        while (currentHealth < maxHealth)
        {
            currentHealth += regenerationAmount;
            Mathf.Clamp(currentHealth, 0, maxHealth);

            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds(1);
        }

        // Сбрасываем ссылку на корутину после завершения регенерации
        healingCoroutine = null;
    }

    public void IncreaseMaxHealth(float HP) 
    {
        maxHealth += HP;
        healthBar.SetMaxHealth(maxHealth);
    }
}
