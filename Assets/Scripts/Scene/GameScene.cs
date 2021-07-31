using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();
        base._sceneType = Define.SceneType.Game;
        
        GameObject dungeon = Managers.Dungion.CreateNewDungeon(Managers.Game._currentWorld);

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player/Player",dungeon);
    }
    public override void Clear()
    {
    }

}
