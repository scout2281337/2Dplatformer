using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    [Header("STATS")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public Slider xpBar;
    public TextMeshProUGUI tmptext;
    public GameObject SkillMenu;
    
    
    [Header("TEXT")]
    public TextMeshProUGUI Skill1;
    public TextMeshProUGUI Skill2;
    public TextMeshProUGUI Skill3;

    [Header("BUTTONS")]
    public Button button1;
    public Button button2;
    public Button button3;

    [Header("ForSkills")]
    private PlayerMovement pm;

    // List of all perks
    private List<Perk> allPerks = new List<Perk>();

    private void Start()
    {
        // Инициализация перков
        allPerks.Add(new Perk("Increase Health", "Increase max health by 20", IncreaseHealth));
        allPerks.Add(new Perk("Increase Attack", "Increase attack by 10%", IncreaseAttack));
        allPerks.Add(new Perk("Increase Speed", "Increase movement speed by 10%", IncreaseSpeed));
        allPerks.Add(new Perk("Double Jump", "Allows double jump", EnableDoubleJump));
        allPerks.Add(new Perk("Regeneration", "Regenerate health over time", EnableRegeneration));

        // Можно добавить больше перков
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        xpBar.value = (float)currentXP / xpToNextLevel;
        tmptext.text = "Level: " + currentLevel;
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP = currentXP - xpToNextLevel;
        xpToNextLevel += 50; // Increase XP required for each new level

        ShowMenu();
    }

    public void ShowMenu()
    {
        SkillMenu.SetActive(true);
        Time.timeScale = 0f;

        // Randomly select 3 different perks
        List<Perk> randomPerks = GetRandomPerks(3);

        // Assign the perks to the skill buttons and set listeners
        Skill1.text = randomPerks[0].Name;
        Skill2.text = randomPerks[1].Name;
        Skill3.text = randomPerks[2].Name;

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(() => SelectPerk(randomPerks[0]));

        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(() => SelectPerk(randomPerks[1]));

        button3.onClick.RemoveAllListeners();
        button3.onClick.AddListener(() => SelectPerk(randomPerks[2]));
    }

    public void CloseMenu()
    {
        SkillMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private List<Perk> GetRandomPerks(int numberOfPerks)
    {
        List<Perk> selectedPerks = new List<Perk>();
        List<Perk> availablePerks = new List<Perk>(allPerks);

        for (int i = 0; i < numberOfPerks; i++)
        {
            int randomIndex = Random.Range(0, availablePerks.Count);
            selectedPerks.Add(availablePerks[randomIndex]);
            availablePerks.RemoveAt(randomIndex);
        }

        return selectedPerks;
    }

    private void SelectPerk(Perk perk)
    {
        perk.ApplyEffect();
        CloseMenu();
    }

    // Примеры эффектов перков
    private void IncreaseHealth()
    {
        Debug.Log("Increased Health by 20");
        // Логика для увеличения здоровья
    }

    private void IncreaseAttack()
    {
        Debug.Log("Increased Attack by 10%");
        // Логика для увеличения атаки

    }

    private void IncreaseSpeed()
    {
        Debug.Log("Increased Speed by 10%");
        // Логика для увеличения скорости
        pm.maxSpeed = pm.maxSpeed * 3f;
    }

    private void EnableDoubleJump()
    {
        Debug.Log("Double Jump enabled");
        // Логика для активации двойного прыжка
    }

    private void EnableRegeneration()
    {
        Debug.Log("Health Regeneration enabled");
        // Логика для регенерации здоровья
    }
}
