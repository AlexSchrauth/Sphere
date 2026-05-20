using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    // Drag your Boss GameObject into this slot in the Inspector
    public BossHealth bossScript; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that walked into the zone is the Player
        if (other.CompareTag("Player"))
        {
            // Tell the boss to wake up the UI!
            if (bossScript != null)
            {
                bossScript.TriggerBossFight();
            }

            // Destroy this trigger zone so the fight doesn't trigger twice
            Destroy(gameObject);
        }
    }
}