using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float chasedMoveSpeed = 4.5f;
    public bool isChased = false;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float rotationSmoothTime = 0.1f;


    [Header("Camera Bop")]
    [SerializeField] private GameObject cam;
    [SerializeField] private float bopIntensity = 0.05f;
    [SerializeField] private float bopSpeed = 10f;
    [SerializeField] private float bopIncreaseWhenMoving = 0.02f;
    [SerializeField] private float bopIncreaseSpeedWhenMoving = 0.02f;

    [Header("Camera Shake")]
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeFrequency = 10.0f;
    [SerializeField] private float shakeSmoothness = 1.0f;

    private CharacterController characterController;
    private Vector2 inputAxis;
    private Vector2 mouseAxis;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    private float cameraPitch = 0.0f;

    private Vector3 originalCamPosition;
    private float shakeTimer;
    private Vector3 currentShakeOffset;

    public bool cameraControlEnabled = true;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalCamPosition = cam.transform.localPosition;
    }

    private void LateUpdate()
    {
        if (cameraControlEnabled)
        {
            // Player movement
            float currentMoveSpeed = isChased ? chasedMoveSpeed : moveSpeed;
            inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 moveDirection = transform.TransformDirection(inputAxis.x, 0, inputAxis.y) * currentMoveSpeed;
            characterController.SimpleMove(moveDirection);

            // Mouse look
            mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseAxis, ref currentMouseDeltaVelocity, rotationSmoothTime);

            cameraPitch -= currentMouseDelta.y;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
            transform.Rotate(Vector3.up * currentMouseDelta.x);

            // Camera bop
            float bopAmount = bopIntensity;
            float bobWalk = bopSpeed;
            if (inputAxis.magnitude > 0)
            {
                bopAmount += bopIncreaseWhenMoving;
                bobWalk += bopIncreaseSpeedWhenMoving;
            }

            float newYPos = originalCamPosition.y + Mathf.Sin(Time.time * bobWalk) * bopAmount;
            cam.transform.localPosition = new Vector3(originalCamPosition.x, newYPos, originalCamPosition.z);

            // Camera shake
            if (isChased && inputAxis.magnitude > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0)
                {
                    StartCoroutine(ShakeCamera());
                }
            }
            else
            {
                shakeTimer = 1f / shakeFrequency;
                currentShakeOffset = Vector3.Lerp(currentShakeOffset, Vector3.zero, Time.deltaTime / shakeSmoothness);
            }
        }
        else
        {
            // Reset player velocity when not allowed to move
            characterController.SimpleMove(Vector3.zero);
        }
    }

    private IEnumerator ShakeCamera()
    {
        Vector3 targetShakeOffset = (Vector3)Random.insideUnitCircle * shakeIntensity;
        float elapsed = 0.0f;

        while (elapsed < shakeSmoothness)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / shakeSmoothness;

            currentShakeOffset = Vector3.Lerp(currentShakeOffset, targetShakeOffset, progress);
            cam.transform.localPosition = new Vector3(originalCamPosition.x, cam.transform.localPosition.y, originalCamPosition.z) + currentShakeOffset;

            yield return null;
        }

        currentShakeOffset = targetShakeOffset;
    }

    public void SetVelocity(Vector3 velocity)
    {
        characterController.Move(velocity);
    }

}
