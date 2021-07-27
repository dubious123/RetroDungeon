using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    public override void Init()
    {
        base.Init();
        base._sceneType = Define.SceneType.Game;
        GameObject Dungeon = Managers.Dungion.CreateNewDungeon(Managers.Game._currentWorld);
    }
    public override void Clear()
    {
    }

}
