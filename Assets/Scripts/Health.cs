using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject DeathPanel;
    public int MaxHealth = 100;
    [SerializeField] private int Currenthealth;
    public HealthBar Healthbar;
    public float RegenerationSpeedTime = 1;
    private PlayerMovement playerMovement;

    // Для неуязвимости
    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;

    // Переменная для отслеживания корутины регенерации
    private Coroutine healingCoroutine;

    void Start()
    {
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
        playerMovement = GetComponent<PlayerMovement>();
        healingCoroutine = StartCoroutine(Healing());
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        // Наносим урон
        Currenthealth -= damage;
        Healthbar.SetHealth(Currenthealth);

        // Останавливаем корутину регенерации, если получаем урон
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }

        // Запускаем корутину неуязвимости
        StartCoroutine(Invincibility());

        // Проверка на смерть
        if (Currenthealth <= 0)
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
        DeathPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Healing()
    {
        while (Currenthealth < MaxHealth)
        {
            Currenthealth++;
            Healthbar.SetHealth(Currenthealth);
            yield return new WaitForSeconds(RegenerationSpeedTime);
        }

        // Сбрасываем ссылку на корутину после завершения регенерации
        healingCoroutine = null;
    }

    public void IncreaseMaxHealth(int HP) 
    {
        MaxHealth += HP;
        Healthbar.SetMaxHealth(MaxHealth);
    
    }
}
