using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();
        base._sceneType = Define.SceneType.Game;
        
        GameObject dungeon = Managers.DungeonMgr.CreateNewDungeon(Managers.GameMgr.CurrentWorld);

        GameObject player = Managers.GameMgr.CreatePlayer(dungeon);

        Managers.GameMgr.StartGame();
    }
    public override void Clear()
    {
    }

}
