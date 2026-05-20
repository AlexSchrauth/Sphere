using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashDistance = 20f;

    // This creates a physical button on your game screen to bypass the keyboard
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "FORCE DASH"))
        {
            DoTheDash();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoTheDash();
        }
    }

    void DoTheDash()
    {
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            transform.position += transform.forward * dashDistance;
            cc.enabled = true;
            Debug.Log("Dash Executed!");
        }
        else
        {
            Debug.LogError("No CharacterController found on " + gameObject.name);
        }
    }
}