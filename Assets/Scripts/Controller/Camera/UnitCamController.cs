using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCamController : MonoBehaviour
{
    [SerializeField] Vector3 _presetPos;
    Vector3 _pos;
    private void Start()
    {
        _pos = _presetPos;
    }
    private void LateUpdate()
    {
        transform.position = _pos;
    }
    public void SetPosition(Vector3 newPos)
    {
        _pos = newPos + _presetPos;
    }
}
