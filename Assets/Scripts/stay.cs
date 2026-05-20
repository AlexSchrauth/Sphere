using UnityEngine;

public class EyeFix : MonoBehaviour
{
    // Adjust this number in the Inspector to move the eye forward or back
    public float offsetZ = 5f; 

    void Start()
    {
        // This forces the eye to the surface the moment Play is pressed
        transform.localPosition = new Vector3(0, 0, offsetZ);
    }
}