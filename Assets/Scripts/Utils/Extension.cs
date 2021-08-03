using Priority_Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    public static Transform[] GetChildren(this GameObject go)
    {
        return Util.GetChildren(go);
    }
    public static Vector3 ConvertToCart(this Vector3 iso,int z = 0)
    {
        return Util.ConvertToCart(iso , z);
    }
    public static Vector3 ConvertToCart(this Vector2 iso, int z = 0)
    {
        return Util.ConvertToCart(iso, z);
    }
    //public static Vector3 ConvertToIso(this Vector3 cart, int z = 0)
    //{
    //    return Util.ConvertToIso(cart, z);
    //}
    //public static Vector3 ConvertToIso(this Vector2 cart, int z = 0)
    //{
    //    return Util.ConvertToIso(cart, z);
    //}
    public static void SetTile(this Tilemap[] tilemaps, Vector3Int tileCartPos, TileInfo tileInfo)
    {
        Util.SetTile(tilemaps, tileCartPos, tileInfo);
    }
    #region Priority Queue Extensions
    public static void Enqueue<TItem,TPriority>(this SimplePriorityQueue<TItem,TPriority> simplePriorityQueue, TItem item) where TItem : Interface.ICustomPriorityQueueNode<TPriority>
    {
        simplePriorityQueue.Enqueue(item, item.GetPriority());
    }
    #endregion
}
