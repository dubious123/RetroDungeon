using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
    Define.Turn _currentTurn;
    bool _somethingHappened;
    public Define.Turn CurrentTurn { get { SetTurn(); return _currentTurn; } }
    public void Init()
    {
        _currentTurn = Define.Turn.Player;
        
    }
    public void OnUpdate()
    {

    }
    public void SetTurn()
    {
        if (_currentTurn == Define.Turn.Player)
        {
            if (Managers.GameMgr.PlayerData.Ap > 0)
            {

            }
            else
            {
                _currentTurn = Define.Turn.Enemy;
            }
        }
        else if (_currentTurn == Define.Turn.Enemy)
        {

        }
    }

}
