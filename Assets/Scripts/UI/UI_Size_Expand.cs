using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Size_Expand : MonoBehaviour
{
    [SerializeField] RectTransform _rect;
    [SerializeField] GUI _GUI;
    [SerializeField] float _Expand_V_Speed;
    [SerializeField] RectTransform _Expand_V_To;
    float _expand_v_delta;
    float _expand_v_ratio;
    void Start()
    {
        _rect.sizeDelta = new Vector2(_rect.sizeDelta.x, 0.1f);
        _expand_v_delta = 0;
        _GUI.enabled = true;
        _GUI.GUIEvent.AddListener(ExpandVertical);
    }

    void ExpandVertical()
    {
        _expand_v_delta += Time.deltaTime;
        _expand_v_ratio = _expand_v_delta * _Expand_V_Speed;
        if (_expand_v_ratio < 1) { _rect.sizeDelta = new Vector2(_rect.sizeDelta.x, Mathf.Lerp(0, _Expand_V_To.sizeDelta.y, _expand_v_ratio)); }
        else
        {
            _rect.sizeDelta = new Vector2(_rect.sizeDelta.x, _Expand_V_To.sizeDelta.y);
            _GUI.GUIEvent.RemoveAllListeners();
            _GUI.enabled = false;
        }
    }
}
