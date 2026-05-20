using UnityEngine;
using UnityEngine.SceneManagement;

public class KillAndRestart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // DEBUG: This prints every time ANY collider touches this trigger
        Debug.Log("Something hit me: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Restarting...");
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        // If this doesn't work, make sure your scene is in File > Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}