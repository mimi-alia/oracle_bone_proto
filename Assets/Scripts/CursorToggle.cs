using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    public PlayerController playerController; // Drag your player object here
    public DrawOnPlane drawOnPlane;           // Drag your DrawOnPlane object here

    void Start()
    {
        // Lock and hide cursor at start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerController != null)
            playerController.allowMovement = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool isCursorVisible = Cursor.visible;

        // Toggle cursor visibility
        Cursor.visible = !isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.Locked : CursorLockMode.None;

        // Enable/disable camera movement
        if (playerController != null)
            playerController.allowMovement = isCursorVisible;

        // Clear drawn lines when hiding the cursor
        if (!Cursor.visible && drawOnPlane != null)
        {
            drawOnPlane.ClearAllLines();
        }
    }
}