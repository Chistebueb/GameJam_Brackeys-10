using UnityEngine;

public class FollowObjectWithOffset : MonoBehaviour
{
    public GameObject objectToFollow;
    public GameObject referenceObject;
    private Vector3 initialOffset;
    private bool isFollowing = false;
    public GameObject e;

    void Start()
    {
        if (objectToFollow != null && referenceObject != null)
        {
            initialOffset = objectToFollow.transform.position - referenceObject.transform.position;
        }
        else
        {
            Debug.LogError("Please assign objectToFollow and referenceObject in the inspector!");
            enabled = false; // Disable the script if references are not set
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFollowing = true;
            initialOffset = objectToFollow.transform.position - referenceObject.transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFollowing = false;
        }

        if (isFollowing && !e.GetComponent<SphereFollowMouse>().getIsBeyondMaxDistance())
        {
            Vector3 targetPosition = referenceObject.transform.position + initialOffset;
            objectToFollow.transform.position = Vector3.Lerp(objectToFollow.transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}