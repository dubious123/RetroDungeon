using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();
        _sceneType = Define.SceneType.Game;
        Managers.WorldMgr.CreateWorld();

        Managers.GameMgr.CreatePlayer();

        Managers.GameMgr.StartGame();
    }
    public override void Clear()
    {
    }

}
