using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillSystem : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("If true, the player object is destroyed. If false, the scene just restarts.")]
    public bool destroyPlayer = true;

    // Use this if 'Is Trigger' is UNCHECKED (Physical collision)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.gameObject);
        }
    }

    // Use this if 'Is Trigger' is CHECKED (Pass-through zone)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerDeath(other.gameObject);
        }
    }

    private void HandlePlayerDeath(GameObject player)
    {
        Debug.Log("Player killed!");

        if (destroyPlayer)
        {
            Destroy(player);
        }

        // Reloads the level to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
