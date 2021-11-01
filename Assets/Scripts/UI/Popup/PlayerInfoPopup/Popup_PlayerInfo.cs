using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup_PlayerInfo : MonoBehaviour
{
    UnityEvent _keyPressedEvent;
    public InventoryController Inventory;
    void OnEnable()
    {
        _keyPressedEvent = Managers.InputMgr.GameController.PlayerInfoEvent;
        _keyPressedEvent.RemoveAllListeners();
        _keyPressedEvent.AddListener(() => gameObject.SetActive(false));
        Managers.UI_Mgr.DownPanel.DeactivateAll();
    }
    private void OnDisable()
    {
        _keyPressedEvent.RemoveAllListeners();
        _keyPressedEvent.AddListener(() => Managers.UI_Mgr.ShowPopup_PlayerInfo());
        Managers.UI_Mgr.DownPanel.ActivateAll();
    }
}
