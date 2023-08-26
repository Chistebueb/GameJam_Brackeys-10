using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 3 * 2;
        mousePos.y -= Screen.height / 3 * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
