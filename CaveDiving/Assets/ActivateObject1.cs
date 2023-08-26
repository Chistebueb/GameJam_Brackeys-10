using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject1 : MonoBehaviour
{
    public GameObject GameObjectOn;

    public void Activate()
    {
        GameObjectOn.SetActive(true);
    }
}
