using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void QuitToMainMenu()
    {
        Managers.Clear();
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
}
