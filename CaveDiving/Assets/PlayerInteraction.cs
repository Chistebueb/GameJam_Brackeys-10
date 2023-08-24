using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInteraction : MonoBehaviour
{
    public float objectDistance = 2f;
    public float rotationSpeed = 2f;
    private GameObject currentObject;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isInspecting;
    private PlayerMovement playerMovement;
    private Camera mainCamera;
    public GameObject PostProcess1;
    public GameObject PostProcess2;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        // Get the main camera component
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure there's a camera with the 'MainCamera' tag in the scene.");
        }
    }

    private void Update()
    {
        if (isInspecting)
        {
            if (Input.anyKeyDown)
            {
                if (currentObject != null) // Add this check
                {
                    StopInspecting();
                }
            }
            else if (currentObject != null)
            {
                RotateInspectedObject();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
            {
                StartInspecting();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            currentObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            currentObject = null;
        }
    }

    private bool firstFrame = true;

    private void StartInspecting()
    {
        firstFrame = true;

        PostProcess1.SetActive(false);
        PostProcess2.SetActive(true);
        isInspecting = true;
        //playerMovement.enabled = false;
        playerMovement.cameraControlEnabled = false;
        playerMovement.SetVelocity(Vector3.zero); 
        originalPosition = currentObject.transform.position;
        originalRotation = currentObject.transform.rotation;

        Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * objectDistance;
        currentObject.transform.position = newPosition;

        Vector3 directionToCamera = mainCamera.transform.position - currentObject.transform.position;
        Vector3 directionToCameraProjected = Vector3.ProjectOnPlane(directionToCamera, Vector3.up);
        Quaternion targetRotation = Quaternion.LookRotation(-directionToCameraProjected);
        currentObject.transform.rotation = targetRotation * originalRotation;

        //currentObject.transform.LookAt(mainCamera.transform.position);
        currentObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Disable the collider while inspecting
        Collider objectCollider = currentObject.GetComponent<Collider>();
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
    }

    private void StopInspecting()
    {
        PostProcess1.SetActive(true);
        PostProcess2.SetActive(false);
        isInspecting = false;
        //playerMovement.enabled = true;
        playerMovement.cameraControlEnabled = true; // Add this line
        currentObject.transform.position = originalPosition;
        currentObject.transform.rotation = originalRotation;

        // Re-enable the collider after inspecting
        Collider objectCollider = currentObject.GetComponent<Collider>();
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
        }

        currentObject = null;
    }


    private void RotateInspectedObject()
    {
        if (firstFrame)
        {
            currentObject.transform.rotation = Quaternion.LookRotation(currentObject.transform.position - mainCamera.transform.position);
            firstFrame = false;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationSpeedX = mouseX * rotationSpeed * Time.deltaTime;
        float rotationSpeedY = mouseY * rotationSpeed * Time.deltaTime;

        currentObject.transform.Rotate(mainCamera.transform.up, -rotationSpeedX, Space.World);
        currentObject.transform.Rotate(mainCamera.transform.right, rotationSpeedY, Space.World);
    }
}