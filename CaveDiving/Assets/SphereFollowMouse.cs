using UnityEngine;
using System.Collections;

public class SphereFollowMouse : MonoBehaviour
{
    public Transform plane; // Reference to the plane's Transform
    public GameObject sphere; // Reference to the sphere GameObject
    public float smoothSpeed = 10f; // Speed of sphere movement
    public float maxDistanceFromCamera = 10f; // Maximum distance from camera

    public static bool isBeyondMaxDistance { get; private set; }

    private Camera mainCamera; // Reference to the main camera

    private bool isGrabbing = false;
    private float privateSmoothSpeed;

    public GameObject sphereModelOn;
    public GameObject sphereModelOff;


    private void Start()
    {
        mainCamera = Camera.main;
        sphereModelOff.SetActive(false);
        sphereModelOn.SetActive(true);
    }

    private void Update()
    {
        // Start a coroutine to smoothly move the sphere
        StartCoroutine(MoveSphereToMouse());

        if (Input.GetMouseButtonDown(0))
        {
            isGrabbing = true;
            privateSmoothSpeed = smoothSpeed;
            smoothSpeed = smoothSpeed/5;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabbing = false;
            smoothSpeed = privateSmoothSpeed;
        }

        if(isBeyondMaxDistance)
        {
            sphereModelOff.SetActive(true);
            sphereModelOn.SetActive(false);
        }
        else
        {
            sphereModelOff.SetActive(false);
            sphereModelOn.SetActive(true);
        }
    }

    private IEnumerator MoveSphereToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.gameObject == plane.gameObject)
        {
            Vector3 targetPosition = hit.point;

            // Calculate the distance from the camera
            float distanceFromCamera = Vector3.Distance(targetPosition, mainCamera.transform.position);

            // Update the isBeyondMaxDistance flag based on distance from camera
            isBeyondMaxDistance = distanceFromCamera > maxDistanceFromCamera;

            // Clamp the distance to the specified maximum
            if (isBeyondMaxDistance)
            {
                Vector3 cameraToTarget = targetPosition - mainCamera.transform.position;
                targetPosition = mainCamera.transform.position + cameraToTarget.normalized * maxDistanceFromCamera;
            }

            Vector3 smoothedPosition = Vector3.Lerp(sphere.transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            sphere.transform.position = smoothedPosition;
        }

        yield return null;
    }

    public bool getIsBeyondMaxDistance()
    {
        return isBeyondMaxDistance;
    }
}