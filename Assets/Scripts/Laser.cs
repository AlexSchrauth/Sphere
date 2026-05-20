using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [Header("Setup")]
    public LineRenderer lineRenderer;
    public GameObject sparkPrefab; 
    public float laserRange = 50f;

    [Header("Timing Settings")]
    public float fireDuration = 3f;
    public float reloadDuration = 2f;
    public float warningTime = 1f;

    [Header("Visual Settings")]
    public Color warningColor = Color.white;
    public Color fireColor = Color.red;
    public float warningWidth = 0.02f;
    public float fireWidth = 0.3f;

    private float timer;
    private bool isFiring = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (isFiring && timer >= fireDuration)
        {
            isFiring = false;
            timer = 0;
            lineRenderer.enabled = false; 
        }
        else if (!isFiring && timer >= reloadDuration)
        {
            isFiring = true;
            timer = 0;
            lineRenderer.enabled = true;
        }

        if (isFiring)
        {
            UpdateLaserVisuals();
            ShootLaser();
        }
    }

    void UpdateLaserVisuals()
    {
        if (timer < warningTime)
        {
            lineRenderer.startColor = warningColor;
            lineRenderer.endColor = warningColor;
            lineRenderer.startWidth = warningWidth;
            lineRenderer.endWidth = warningWidth;
        }
        else
        {
            lineRenderer.startColor = fireColor;
            lineRenderer.endColor = fireColor;
            lineRenderer.startWidth = fireWidth;
            lineRenderer.endWidth = fireWidth;
        }
    }

    void ShootLaser()
    {
        Vector3 startPos = transform.position;
        lineRenderer.SetPosition(0, startPos);

        RaycastHit hit;
        if (Physics.Raycast(startPos, transform.forward, out hit, laserRange))
        {
            lineRenderer.SetPosition(1, hit.point);

            // --- DAMAGE SECTION ---
            if (timer >= warningTime) 
            {
                // This line tries to "find" the player health on the object we hit
                PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();

                if (player != null)
                {
                    player.TakeDamage(50f * Time.deltaTime);
                }

                // Sparks
                if (sparkPrefab != null)
                {
                    Instantiate(sparkPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            // -----------------------
        }
        else
        {
            lineRenderer.SetPosition(1, startPos + (transform.forward * laserRange));
        }
    }
}