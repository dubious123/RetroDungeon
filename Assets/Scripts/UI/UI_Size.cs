using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Size : MonoBehaviour
{
    [SerializeField] Button _Btn;
    [SerializeField] float _Expand_V_From;
    [SerializeField] float _Expand_V_To;
    [SerializeField] float _Expand_V_Speed;

    float _expand_v_delta;
    float _expand_v_ratio;
    [SerializeField] RectTransform _rect;
    [SerializeField] GUI _GUI;
    public void ExpandVertical()
    {
        _expand_v_delta = 0;
        _GUI.enabled = true;
        _GUI.GUIEvent.AddListener(_ExpandVertical);
    }
    private void _ExpandVertical()
    {

        _expand_v_delta += Time.deltaTime;
        _expand_v_ratio = _expand_v_delta * _Expand_V_Speed;
        if (_expand_v_ratio < 1) { _rect.anchorMin = new Vector2(0, Mathf.Lerp(_Expand_V_From, _Expand_V_To, _expand_v_ratio)); }
        else
        {
            _rect.anchorMin = new Vector2(0, _Expand_V_To);
            _GUI.GUIEvent.RemoveAllListeners();
            _GUI.enabled = false;
            _Btn.onClick.RemoveListener(ExpandVertical);
            _Btn.onClick.AddListener(ContractVertical);
        }
    }
    public void ContractVertical()
    {
        _expand_v_delta = 0;
        _GUI.enabled = true;
        _GUI.GUIEvent.AddListener(_ContractVertical);
    }
    private void _ContractVertical()
    {
        _expand_v_delta += Time.deltaTime;
        _expand_v_ratio = _expand_v_delta * _Expand_V_Speed;
        if (_expand_v_ratio < 1) { _rect.anchorMin = new Vector2(0, Mathf.Lerp(_Expand_V_To, _Expand_V_From, _expand_v_ratio)); }
        else
        {
            _rect.anchorMin = new Vector2(0, _Expand_V_From);
            _GUI.GUIEvent.RemoveAllListeners();
            _GUI.enabled = false;
            _Btn.onClick.RemoveListener(ContractVertical);
            _Btn.onClick.AddListener(ExpandVertical);
        }
    }
}

