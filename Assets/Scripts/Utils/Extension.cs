using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static Vector3 ConvertToIso(this Vector3 cart, int z = 0)
    {
        return Util.ConvertToIso(cart, z);
    }
    public static Vector3 ConvertToIso(this Vector2 cart, int z = 0)
    {
        return Util.ConvertToIso(cart, z);
    }
}
