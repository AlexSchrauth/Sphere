using UnityEngine;
using System.Collections;

public class CryoGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 20f;
    public float range = 50f;
    public float fireRate = 0.25f;
    private float nextFireTime;

    [Header("Laser Visuals")]
    private LineRenderer lineRenderer; 
    public float laserDuration = 0.1f; 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            ShootCryoLaser();
        }
    }

    void ShootCryoLaser()
    {
        if (lineRenderer != null)
        {
            StartCoroutine(RenderLaserBeam());
        }

        // Create the invisible ray shooting forward from the camera's center
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Force the laser visual to start right at the camera's position
        if (lineRenderer != null)
        {
            // Nudge it slightly downward so it doesn't block the exact center of your view
          Vector3 cameraFaceOrigin = Camera.main.transform.position + (-Camera.main.transform.up * 0.2f);
            lineRenderer.SetPosition(0, cameraFaceOrigin);
        }

        // Check if the raycast hits an object
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            // Deal damage if it hits the boss
            BossHealth boss = hit.collider.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
        else
        {
            // If it misses, extend the laser beam straight forward into the air
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, ray.origin + (ray.direction * range));
            }
        }
    }

    IEnumerator RenderLaserBeam()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lineRenderer.enabled = false;
    }
}