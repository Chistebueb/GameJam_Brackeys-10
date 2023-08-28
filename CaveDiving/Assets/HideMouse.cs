using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Screen.fullScreen = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the MainMenu scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
