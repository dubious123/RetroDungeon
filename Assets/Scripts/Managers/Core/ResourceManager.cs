using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    /// <summary>
    /// if T is GameObject, try to Get Original from Pool
    /// if T is not the type of gameObject, Load it from its path
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >= 0)
            {
                name = name.Substring(index + 1);
            }
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null) { return go as T; }
        }
        return Resources.Load<T>(path);
    }
    /// <summary>
    /// prefabs�� �����´�. 
    /// </summary>
    /// <param name="path">Resources/Prefabs/�Ʒ��� �ִ� �������� ���</param>
    /// <param name="parent">������ �������� ���� �θ�</param>
    /// <returns></returns>
    public GameObject Instantiate(string path,Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }
    public Sprite GetSprite(string path)
    {
        //To Do
        Sprite sprite = Load<Sprite>($"Sprites/{path}");
        if (sprite == null)
        {
            Debug.Log($"Failed to load sprite : {path}");
            return null;
        }
        return sprite;
    }
    public void Destroy(GameObject go)
    {
        if (go == null) { return; }
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go);
    }
}