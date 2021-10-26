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
        Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);
        Managers.InputMgr.GameController.RightClickEvent.AddListener(Cancel);
    }
    void SelectUnit(InputAction.CallbackContext context)
    {
        Managers.InputMgr.GameInputSystem.actions["OnMouseClick"].performed -= SelectUnit;
        _selectedUnit = Managers.GameMgr.GetUnit(Managers.InputMgr.GetMouseCellPos());

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
        Vector3Int cellpos = Managers.InputMgr.GetMouseCellPos();
        if (!Managers.GameMgr.HasTile(cellpos) || _selectedUnit == null) { return; }
        _selectedUnit.GetComponent<BaseUnitData>().CurrentCellCoor = cellpos;
        Managers.GameMgr.SetUnit(_selectedUnit,cellpos);
        _Content.SetActive(true);   
        Managers.GameMgr.Player_Controller.HandleIdle();
    }
}
