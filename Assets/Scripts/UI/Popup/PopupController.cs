using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject _UnitStatusBar;
    public GameObject _TileInfo;
    public GameObject _TileInfo_SkillBox;
    public GameObject _GetSkill;
    public GameObject _PlayerDeath;
    public GameObject _PlayerInfo;
    public GameObject _SlotContentInfo;
    public void Init_GamePopups()
    {
        Managers.InputMgr.GameController.PlayerInfoEvent.AddListener(() => Managers.UI_Mgr.ShowPopup_PlayerInfo());
        Managers.PoolMgr.CreatePool(_GetSkill, 1);
        Managers.PoolMgr.CreatePool(_UnitStatusBar, 1);
        Managers.PoolMgr.CreatePool(_TileInfo, 1);
    }
    public void Clear()
    {

    }
}
