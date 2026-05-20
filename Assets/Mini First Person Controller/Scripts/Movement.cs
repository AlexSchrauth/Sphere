using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform player;
    public float moveSpeed = 5.0f;
    public float stoppingDistance = 10.0f;
    public float retreatDistance = 5.0f;

    [Header("Hover Settings")]
    public float hoverAmplitude = 0.5f;
    public float hoverFrequency = 1.0f;

    [Header("Status")]
    [Range(0, 1)] public float freezeAmount = 0f; // 0 is normal, 1 is stuck

    void Update()
    {
        if (player == null) return;

        // 1. Calculate Distance
        float distance = Vector3.Distance(transform.position, player.position);

        // 2. Adjust Speed based on "Freeze"
        // If freezeAmount is 0.8, speed is 20%. If it's 1, speed is 0.
        float currentSpeed = moveSpeed * (1.0f - freezeAmount);

        // 3. Follow/Retreat Logic
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
        else if (distance < retreatDistance)
        {
            // Retreats if the player gets too close
            transform.position = Vector3.MoveTowards(transform.position, player.position, -currentSpeed * Time.deltaTime);
        }

        // 4. The "Hover" (Fixed Logic)
        // We use Sine to move up and down, but we multiply by currentSpeed 
        // so if the boss is frozen, the wobble stops too!
        float hoverY = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.Translate(Vector3.up * hoverY * Time.deltaTime * (1.0f - freezeAmount));

        // 5. Always look at the player
        transform.LookAt(player);
    }

    // --- ADD THIS SO YOUR GUN CAN WORK ---
    public void ApplyFreeze(float amount)
    {
        freezeAmount = amount;
        // After 3 seconds, call the Melt function to move again
        CancelInvoke("Melt");
        Invoke("Melt", 3f);
    }

    void Melt()
    {
        freezeAmount = 0f;
    }
}