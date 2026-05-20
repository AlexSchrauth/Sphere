using UnityEngine;
using UnityEngine.UI;
using TMPro; // Remove this if you are using regular Unity UI Text instead of TextMeshPro

public class BossUIController : MonoBehaviour
{
    public static BossUIController Instance; // Allows other scripts to easily call this

    [Header("UI References")]
    public GameObject bossUIPanel;         // The parent 'BossUI' object to toggle on/off
    public Slider healthSlider;            // The BossHealthBar slider
    public TextMeshProUGUI bossNameText;   // The BossNameText component (use 'Text' if regular UI)

    void Awake()
    {
        // Simple Singleton setup so the Boss script can find this easily
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Hide the boss UI automatically when the game starts
        HideBossUI();
    }

    // Call this when the player enters the boss arena or triggers the fight
    public void ShowBossUI(string bossName, float maxHealth)
    {
        bossUIPanel.SetActive(true);
        bossNameText.text = bossName;
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Call this from your Boss AI script whenever the boss takes damage
    public void UpdateBossHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    // Call this when the boss dies to clear the screen
    public void HideBossUI()
    {
        bossUIPanel.SetActive(false);
    }
}