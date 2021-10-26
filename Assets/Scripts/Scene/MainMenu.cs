using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : BaseScene
{
    public override void Init()
    {
        base.Init();
        base._sceneType = Define.SceneType.Menu;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
    }
    public override void Clear()
    {

    }
}
