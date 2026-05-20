using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    public float waitTime = 2f;      // How long to wait before sprouting
    public float sproutSpeed = 0.2f; // How fast it comes up (lower is faster)
    public float extendHeight = 2f;  // How high the spike goes
    
    private Vector3 hiddenPos;
    private Vector3 extendedPos;

    void Start()
    {
        // Set the start and end positions based on current location
        hiddenPos = transform.position;
        extendedPos = transform.position + Vector3.up * extendHeight;
        
        // Start the trap cycle
        StartCoroutine(SproutCycle());
    }

    IEnumerator SproutCycle()
    {
        while (true)
        {
            // 1. Wait for the timer
            yield return new WaitForSeconds(waitTime);

            // 2. Sprout UP quickly
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / sproutSpeed;
                transform.position = Vector3.Lerp(hiddenPos, extendedPos, t);
                yield return null;
            }

            // 3. Stay up for a moment
            yield return new WaitForSeconds(1f);

            // 4. Reset DOWN
            transform.position = hiddenPos;
        }
    }
}