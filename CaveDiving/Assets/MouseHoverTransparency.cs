using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverTransparency : MonoBehaviour
{
    private Renderer renderer;
    private Color originalColor;
    public float hoverTransparency = 0.5f; // Set the transparency level here

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    private void OnMouseEnter()
    {
        Color newColor = originalColor;
        newColor.a = hoverTransparency;
        renderer.material.color = newColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = originalColor;
    }
}
