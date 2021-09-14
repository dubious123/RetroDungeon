using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; } //First Duplication of Original prefab
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();
        public void Init(GameObject original, int count) // count : number of copies
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{Original.name}_Root";
            for(int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        /// <summary>
        /// Make copies from original
        /// </summary>
        /// <returns></returns>
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }
        /// <summary>
        /// Push copy to PoolStack, unactivated
        /// </summary>
        /// <param name="poolable"></param>
        public void Push(Poolable copy)
        {
            if(copy == null)
            {
                return;
            }
            if(copy.GetComponent<RectTransform>() == null) { copy.transform.parent = Root; }
            else { copy.transform.SetParent(Root, false); } 
            copy.gameObject.SetActive(false);
            _poolStack.Push(copy);
        }
        /// <summary>
        /// Pop copy from Poolable Stack, Attach it to parent
        /// if Poolable Stack is empty, create new copy and Pop it
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public Poolable Pop(Transform parent)
        {
            Poolable copy;
            if (_poolStack.Count > 0) { copy = _poolStack.Pop(); }
            else { copy = Create(); }
            copy.gameObject.SetActive(true);
            if(parent == null) {
                copy.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(copy.gameObject, SceneManager.GetActiveScene()); 
            }
            else { copy.transform.SetParent(parent); }
            
            return copy;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _poolRoot;
    //create PoolRoot
    public void Init()
    {
        if(_poolRoot == null)
        {
            _poolRoot = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_poolRoot);
        }
    }
    /// <summary>
    /// Create Pool and Add to Dictionary
    /// </summary>
    /// <param name="original"></param>
    /// <param name="count"></param>
    public void CreatePool(GameObject original,int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _poolRoot;
        _pool.Add(original.name, pool);
    }
    /// <summary>
    /// inactivate poolable obj and push to corresponding pool
    /// </summary>
    /// <param name="poolable">Poolable copy</param>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name; 
        if(_pool.ContainsKey(name) == false)
        {
            string path = poolable.GetComponent<IOriginalPath>()?.Path;
            if (path != null) { CreatePool(Resources.Load<GameObject>(path)); }
            GameObject.Destroy(poolable.gameObject); 
            return;
        }
        _pool[name].Push(poolable);
    }
    /// <summary>
    /// bring copy from Pool
    /// if the original is not in the Dictionary, create one
    /// </summary>
    /// <param name="original"></param>
    /// <param name="parent"></param>
    /// <param name="count">number of copies</param>
    /// <returns></returns>
    public Poolable Pop(GameObject original,Transform parent = null, int count = 5)
    {
        if(_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original, count);
        }
        return _pool[original.name].Pop(parent);
    }
    public GameObject GetOriginal(string name)
    {
        if(_pool.ContainsKey(name) == false) { return null; }
        return _pool[name].Original;
    }
    public void Clear()
    {
        foreach(Transform child in _poolRoot) { GameObject.Destroy(child.gameObject); }
        _pool.Clear();
    }

}
