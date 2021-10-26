using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void StartNewGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        _loadGameScene(asyncLoad).RunCoroutine();
        Managers.Clear();
    }
    IEnumerator<float> _loadGameScene(AsyncOperation asyncLoad)
    {
        while (!asyncLoad.isDone)
        {
            yield return 0f;
        }
    }
}
