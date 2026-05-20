using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Stats")]
    public string bossName = "THE AGGRESSOR";
    public float maxHealth = 500f;
    private float currentHealth;

    [Header("Settings")]
    public bool startFightOnLoad = true;

    void Start()
    {
        currentHealth = maxHealth;

        // If you want the health bar to pop up immediately when the scene starts
        if (startFightOnLoad)
        {
            TriggerBossFight();
        }
    }

    // Call this to turn the UI on (can be called by a trigger zone later if you want)
    public void TriggerBossFight()
    {
        if (BossUIController.Instance != null)
        {
            BossUIController.Instance.ShowBossUI(bossName, maxHealth);
        }
    }

    // This is the function your player's weapons/projectiles should call when they hit the boss
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(bossName + " took damage! Current Health: " + currentHealth);

        // Update the UI slider
        if (BossUIController.Instance != null)
        {
            BossUIController.Instance.UpdateBossHealth(currentHealth);
        }

        // Check if dead
        if (currentHealth <= 0)
        {
            BossDie();
        }
    }

    void BossDie()
    {
        // Hide the health bar panel
        if (BossUIController.Instance != null)
        {
            BossUIController.Instance.HideBossUI();
        }

        Debug.Log(bossName + " has been defeated!");
        Destroy(gameObject); // Destroys the boss object
    }
}