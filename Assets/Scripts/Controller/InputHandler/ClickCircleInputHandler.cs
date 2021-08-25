using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCircleInputHandler : MonoBehaviour
{
    Material _material;
    GUI _gui;
    float _delta;
    float _speed;
    float _ratio;
    private void Awake()
    {
        _material = GetComponent<Image>().material;
        _material.SetFloat("_Alpha", 0);
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
        _speed = 4f;
    }
    public void Activate()
    {
        _gui.enabled = true;
        _gui.GUIEvent.AddListener(FadeIn);
    }
    public void Deactivate()
    {
        _gui.enabled = true;
        _gui.GUIEvent.AddListener(FadeOut);
    }
    private void FadeIn()
    {
        _delta += Time.deltaTime;
        _ratio = Mathf.Clamp(_delta * _speed, 0, 1);
        if(_ratio > 1) 
        {
            _material.SetFloat("_Alpha", 1);
            _material.SetFloat("_Radius", 0.47f);
            _delta = 0; 
            _gui.GUIEvent.RemoveListener(FadeIn);
            _gui.enabled = false;
            return;
        }
        _material.SetFloat("_Alpha", _ratio);
        _material.SetFloat("_Radius", 0.47f * _ratio);
    }
    private void FadeOut()
    {
        _delta += Time.deltaTime;
        _ratio = 1 - Mathf.Clamp(_delta * _speed, 0, 1);
        if(_ratio < 0)
        {
            _material.SetFloat("_Alpha", 0);
            _material.SetFloat("_Radius", 0);
            _delta = 0;
            _gui.GUIEvent.RemoveListener(FadeIn);
            _gui.enabled = false;
            return;
        }

    }
}
