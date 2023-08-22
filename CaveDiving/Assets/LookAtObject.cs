using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform targetObject;   // The object to look at
    public float xOffset = 0.0f;     // Offset along the x-axis
    public float yOffset = 0.0f;     // Offset along the y-axis
    public float zOffset = -5.0f;    // Offset along the z-axis

    private void Update()
    {
        if (targetObject != null)
        {
            // Calculate the desired position based on the target's position and offsets
            Vector3 desiredPosition = targetObject.position + new Vector3(xOffset, yOffset, zOffset);

            // Set the camera's position to the desired position
            transform.position = desiredPosition;

            // Rotate the camera to look at the target object
            transform.LookAt(targetObject);
        }
        else
        {
            Debug.LogWarning("No target object assigned to LookAtObject script!");
        }
    }
}
