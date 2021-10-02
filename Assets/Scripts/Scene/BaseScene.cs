using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.SceneType _sceneType { get; protected set; } = Define.SceneType.Unknown;
    private void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        //Initialize what All Scene must do
        Object obj = FindObjectOfType(typeof(EventSystem));
        if(obj == null)
        {
            Managers.ResourceMgr.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }
    public abstract void Clear();
}
