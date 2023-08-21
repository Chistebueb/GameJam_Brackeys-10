using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Transform sphere;
    public Transform mainCamera;
    public Transform plane;
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    private Vector3 planeNormal;
    private Vector3 startPosition;

    private void Start()
    {
        planeNormal = plane.up;
        startPosition = sphere.position;
    }

    private void Update()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        // Move sphere up/down along the plane
        Vector3 moveDirection = Vector3.ProjectOnPlane(mainCamera.forward, planeNormal).normalized;
        Vector3 newPosition = sphere.position + moveDirection * mouseDelta.y * moveSpeed;
        newPosition = Vector3.ClampMagnitude(newPosition - startPosition, plane.localScale.x * 0.5f) + startPosition;
        sphere.position = newPosition;

        // Rotate sphere around the camera left/right
        if (mouseDelta.x != 0)
        {
            Quaternion rotation = Quaternion.AngleAxis(mouseDelta.x * rotationSpeed, Vector3.up);
            Vector3 rotatedPosition = rotation * (sphere.position - mainCamera.position) + mainCamera.position;
            Vector3 toCenter = rotatedPosition - mainCamera.position;
            rotatedPosition = Vector3.ClampMagnitude(toCenter, plane.localScale.x * 0.5f) + mainCamera.position;
            sphere.position = rotatedPosition;
            sphere.Rotate(Vector3.up, mouseDelta.x * rotationSpeed, Space.World);
        }
    }
}
