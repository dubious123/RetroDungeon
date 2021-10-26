using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void QuitToMainMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu",LoadSceneMode.Single);
        _loadMainMenu(asyncLoad).RunCoroutine();
        Managers.Clear();
    }
    IEnumerator<float> _loadMainMenu(AsyncOperation asyncLoad)
    {
        while (!asyncLoad.isDone)
        {
            yield return 0f;
        }
    }
}
