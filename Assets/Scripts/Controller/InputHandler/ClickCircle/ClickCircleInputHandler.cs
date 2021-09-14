using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCircleInputHandler : MonoBehaviour
{
    #region GetHandlers
    public ExitInputHandler Exit;
    public InfoInputHandler Info;
    public YesInputHandler Yes;
    public RectTransform[] Children;
    ExitInputHandler _exit;
    InfoInputHandler _info;
    YesInputHandler _yes;
    RectTransform[] _children;
    Vector2[] _btnMoveDir;
    #endregion
    Vector3 _pos;
    Vector3Int _cellPos;
    Material _material;
    GUI _gui;
    float _delta;
    float _speed;
    float _ratio;
    float _maxRadius;
    float _maxBtnMoveDistance;
    int _activeBtnNum;
    private void Awake()
    {
        _material = GetComponent<Image>().material;
        _material.SetFloat("_Alpha", 0);
        _maxRadius = _material.GetFloat("_MaxRadius");
        _maxBtnMoveDistance = GetComponent<RectTransform>().rect.width / 2;
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
        _speed = 4f;
        _children = GetComponentsInChildren<RectTransform>();
        _exit = Exit;
        _info = Info;
        _yes = Yes;

        _btnMoveDir = new Vector2[3];
        _children = Children;
    }
    private void OnGUI()
    {
        transform.position = Managers.CameraMgr.GameCam.WorldToScreenPoint(_pos);
    }
    public void SetPosition(Vector3Int newPos)
    {
        _cellPos = newPos;
        _pos = Managers.GameMgr.Floor.GetCellCenterWorld(newPos);
    }
    private void ResetUI()
    {
        for(int i = 0; i < _activeBtnNum; i++)
        {
            _children[i].anchoredPosition = Vector2.zero;
        }
    }
    public void Activate()
    {
        ResetUI();
        _gui.enabled = true;
        _gui.GUIEvent.AddListener(FadeIn);
    }
    public void Deactivate()
    {
        Managers.UI_Mgr.ResetClickedCell();
        _gui.enabled = true;
        _gui.GUIEvent.AddListener(FadeOut);
        DisableBtns();
    }
    private void FadeIn()
    {
        _delta += Time.deltaTime;
        _ratio = _delta * _speed;
        if(_ratio >= 1.0f) 
        {
            for (int i = 0; i < _activeBtnNum; i++)
            {
                _children[i].anchoredPosition = _btnMoveDir[i] * _maxBtnMoveDistance;
            }
            _material.SetFloat("_Alpha", 1);
            _material.SetFloat("_Radius", _maxRadius);
            _delta = 0; 
            _gui.GUIEvent.RemoveListener(FadeIn);
            _gui.enabled = false;
            return;
        }
        for(int i = 0; i < _activeBtnNum; i++)
        {
            _children[i].anchoredPosition = _btnMoveDir[i] * _maxBtnMoveDistance * _ratio;
        }
        _material.SetFloat("_Alpha", _ratio);
        _material.SetFloat("_Radius", _maxRadius * _ratio);
    }
    private void FadeOut()
    {
        _delta += Time.deltaTime;
        _ratio = 1 - _delta * _speed;
        if(_ratio <= 0)  
        {
            _material.SetFloat("_Alpha", 0);
            _material.SetFloat("_Radius", 0);
            _delta = 0;
            _gui.GUIEvent.RemoveListener(FadeOut);
            _gui.enabled = false;
            return;
        }
        for (int i = 0; i < _activeBtnNum; i++)
        {
            _children[i].anchoredPosition = _btnMoveDir[i] * _maxBtnMoveDistance * _ratio;
        }
        _material.SetFloat("_Alpha", _ratio);
        _material.SetFloat("_Radius", _maxRadius * _ratio);
    }

    private void DisableBtns()
    {
        foreach(RectTransform rt in _children)
        {
            rt.gameObject.SetActive(false);
        }
        _activeBtnNum = 0;
    }

    public void EnableBtns(bool isYesIncluded, GameObject unit = null)
    {
        float radian;
        if (isYesIncluded) { _activeBtnNum = _children.Length; }
        else { _activeBtnNum = _children.Length - 1; Yes.gameObject.SetActive(false); }
        for (int i = 0; i < _activeBtnNum; i++)
        {
            _children[i].gameObject.SetActive(false);
            _children[i].gameObject.SetActive(true);
            radian = (_activeBtnNum - i) * Mathf.PI / (_activeBtnNum + 1);
            _btnMoveDir[i] = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        _exit.ExitEvent.AddListener(Managers.UI_Mgr.ResetClickedCell);
        _yes.YesEvent.AddListener(Managers.UI_Mgr.ResetClickedCell);
        _info.InfoEvent.AddListener(Managers.UI_Mgr.ResetClickedCell);
        _info.InfoEvent.AddListener(() => Managers.UI_Mgr.ShowTilePopup(_cellPos,unit)); 
        _info.InfoEvent.AddListener(() => Managers.ResourceMgr.Destroy(GameObject.Find(Managers.UI_Mgr.UnitStatusBarName)));
        _info.InfoEvent.AddListener(Managers.InputMgr.GameController.DeactivateCameraScroll);
        _exit.ExitEvent.AddListener(() => Managers.ResourceMgr.Destroy(GameObject.Find(Managers.UI_Mgr.UnitStatusBarName)));
    }
}
