using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_MoveUnit : MonoBehaviour
{
    [SerializeField] GameObject _Content;
    GameObject _selectedUnit;
    public void Onclicked()
    {
        DisableAll();
        Debug.Log("Move unit clicked");
        Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed += SelectUnit;
        Managers.InputMgr.GameController.RightClickEvent.AddListener(Cancel);
    }
    void SelectUnit(InputAction.CallbackContext context)
    {
        Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed -= SelectUnit;
        _selectedUnit = Managers.DungeonMgr.GetTileInfoDict()[Managers.GameMgr.Floor.WorldToCell(Managers.InputMgr.MouseWorldPosition)].Unit;

        if (_selectedUnit == null) { return; }
        else 
        { 
            Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed -= MoveUnit;
            Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed += MoveUnit;
        }
    }
    void Cancel()
    {
        _Content.SetActive(true);
        Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed -= SelectUnit;
        Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);

    }
    void DisableAll()
    {
        _Content.SetActive(false);
    }
    void MoveUnit(InputAction.CallbackContext context)
    {
        Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed -= MoveUnit;
        Vector3Int cellpos = Managers.GameMgr.Floor.WorldToCell(Managers.InputMgr.MouseWorldPosition);
        if (!Managers.DungeonMgr.GetTileInfoDict().ContainsKey(cellpos) || _selectedUnit ==null) { return; }
        _selectedUnit.transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(Managers.GameMgr.Floor.WorldToCell(Managers.InputMgr.MouseWorldPosition));
        _Content.SetActive(true);
        Managers.GameMgr.Player_Data.CurrentCellCoor = cellpos;

        Managers.GameMgr.Player_Controller.HandleIdle();
    }
}
