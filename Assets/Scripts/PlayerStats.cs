using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public Slider xpBar;
    public TextMeshProUGUI tmptext;

    private void Update() 
    {
        xpBar.value = (float)currentXP/ xpToNextLevel;
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
        // Call method to improve stats or abilities
        UpgradeStats();
    }

    private void UpgradeStats()
    {
        // Increase stats like health, damage, etc.
    }
}
