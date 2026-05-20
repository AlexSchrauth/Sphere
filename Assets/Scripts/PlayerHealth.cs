using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    void Update()
    {
        // DEBUG: Press K to kill yourself instantly to test the script
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(999);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Current Health: " + health);

        if (health <= 0)
        {
            Debug.Log("DIED - Reloading Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}