using UnityEngine;

public class HeldItemController : MonoBehaviour
{
    public Transform heldItem;                 // The object you're holding
    public Transform cameraTransform;          // Typically the MainCamera
    public Transform raisedTransform;          // Empty GameObject for raised position
    public Transform loweredTransform;         // Empty GameObject for lowered position

    public float itemMoveSpeed = 10f;          // Speed for smoothing movement
    public KeyCode toggleItemKey = KeyCode.Q;  // Toggle key

    public Vector3 rotationOffset = Vector3.zero; // Extra rotation applied to facing

    private bool isItemLowered = true;

    void Update()
    {
        if (Input.GetKeyDown(toggleItemKey))
            isItemLowered = !isItemLowered;

        if (heldItem == null || cameraTransform == null) return;

        Transform targetTransform = isItemLowered ? loweredTransform : raisedTransform;
        if (targetTransform == null) return;

        // Calculate target position and look-at rotation
        Vector3 targetPosition = targetTransform.position;
        Quaternion lookAtCamera = Quaternion.LookRotation(cameraTransform.position - targetPosition);
        Quaternion finalRotation = lookAtCamera * Quaternion.Euler(rotationOffset);

        // Smooth movement and rotation
        heldItem.position = Vector3.Lerp(heldItem.position, targetPosition, Time.deltaTime * itemMoveSpeed);
        heldItem.rotation = Quaternion.Lerp(heldItem.rotation, finalRotation, Time.deltaTime * itemMoveSpeed);
    }
}