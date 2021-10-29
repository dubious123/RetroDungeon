using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup_PlayerInfo : MonoBehaviour
{
    UnityEvent _keyPressedEvent;
    void Awake()
    {
        _keyPressedEvent = Managers.InputMgr.GameController.PlayerInfoEvent;
        _keyPressedEvent.RemoveAllListeners();
        _keyPressedEvent.AddListener(() => Managers.ResourceMgr.Destroy(gameObject));
    }
    private void OnDestroy()
    {
        _keyPressedEvent.RemoveAllListeners();
        _keyPressedEvent.AddListener(() => Managers.UI_Mgr.ShowPopup_PlayerInfo());
    }
}
