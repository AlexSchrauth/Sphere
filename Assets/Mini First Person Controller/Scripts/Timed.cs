using System.Collections;
using UnityEngine;

public class TimedVisibility : MonoBehaviour
{
    [Header("Timing Settings")]
    [SerializeField] float visibleDuration = 3f;
    [SerializeField] float hiddenDuration = 2f;
    [SerializeField] bool loop = true;

    // References to components we want to hide
    private Renderer objectRenderer;
    private Collider objectCollider;

    void Start()
    {
        // Cache the components so we don't have to look for them repeatedly
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        // Safety check: make sure the object actually has a Renderer
        if (objectRenderer == null)
        {
            Debug.LogError("No Renderer found on " + gameObject.name + ". Script won't work!");
            return;
        }

        StartCoroutine(VisibilityCycle());
    }

    private IEnumerator VisibilityCycle()
    {
        do
        {
            // --- APPEAR ---
            SetVisibility(true);
            yield return new WaitForSeconds(visibleDuration);

            // --- DISAPPEAR ---
            SetVisibility(false);
            yield return new WaitForSeconds(hiddenDuration);

        } while (loop);
    }

    // Helper function to turn visual and physical presence on/off
    private void SetVisibility(bool state)
    {
        if (objectRenderer != null) objectRenderer.enabled = state;
        
        // We toggle the collider too so players don't walk on invisible floors
        if (objectCollider != null) objectCollider.enabled = state;
    }
}