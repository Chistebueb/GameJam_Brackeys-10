using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject GameObjectOn;
    public GameObject GameObjectOff;

    public void Activate()
    {
        GameObjectOn.SetActive(true);
        GameObjectOff.SetActive(false);
    }
}
