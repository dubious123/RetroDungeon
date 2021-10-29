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
            GameObject go = Managers.PoolMgr.GetOriginal(name);
            if (go != null) { return go as T; }
        }
        return Resources.Load<T>(path);
    }
    /// <summary>
    /// prefabs을 가져온다. 
    /// </summary>
    /// <param name="path">Resources/Prefabs/아래에 있는 프리팹의 경로</param>
    /// <param name="parent">가져온 프리팹이 붙을 부모</param>
    /// <returns></returns>
    public GameObject Instantiate(string path,Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        if(original.GetComponent<Poolable>() != null)
        {
            return Managers.PoolMgr.Pop(original, parent).gameObject;
        }
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }
    public GameObject Instantiate(GameObject original, Transform parent = null)
    {
        string path = original.GetComponent<IOriginalPath>()?.Path;
        if(path != null) { return Instantiate(path, parent); }
        else if (original.GetComponent<Poolable>()) { return Managers.PoolMgr.Pop(original, parent).gameObject; }
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
    public Sprite GetSkillSprite(string name)
    {
        Sprite sprite = Load<Sprite>("Data/RenderingData/Skill/"+ name);
        if (sprite == null)
        {
            Debug.Log("Failed to load sprite : " + name);
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
            Managers.PoolMgr.Push(poolable); 
            return;
        }
        Object.Destroy(go);
    }
}
