using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnd : MonoBehaviour
{
    public void ChangeToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
