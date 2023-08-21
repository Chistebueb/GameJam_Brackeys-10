using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;    // Reference to the object to follow
    public float smoothSpeed = 0.125f;   // Smoothing factor for movement
    public Vector3 offset;      // Offset between the follower and target

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
