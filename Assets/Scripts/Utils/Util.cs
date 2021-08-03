using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if(component == null) { component = go.AddComponent<T>(); }
        return component;
    }
    public static Transform[] GetChildren(GameObject go)
    {
        int childcount = go.transform.childCount;
        Transform[] arr = null;
        if (childcount > 0)
        {
            arr = new Transform[childcount];
            for (int i = 0; i < childcount; i++)
            {
                arr[i] = go.transform.GetChild(i);
            }
        }
        return arr;
    }

    public static Vector3 ConvertToCart(Vector3 iso, int z = 0)
    {
        Vector3 cart = new Vector3();
        cart.z = z;
        cart.x = (2.0f * iso.y + iso.x) * 0.5f;
        cart.y = (2.0f * iso.y - iso.x) * 0.5f;
        return cart;
    }
    public static Vector3 ConvertToCart(Vector2 iso, int z = 0)
    {
        Vector3 cart = new Vector3();
        cart.z = z;
        cart.x = (2.0f * iso.y + iso.x) * 0.5f;
        cart.y = (2.0f * iso.y - iso.x) * 0.5f;
        return cart;
    }
    //public static Vector3 ConvertToIso(Vector3 cart, int z = 0)
    //{
    //    Vector3 iso = new Vector3();
    //    iso = cart.x * Define._tileIsoDir.RightDown + cart.y * Define._tileIsoDir.RightUp;
    //    iso.z = z;
    //    return iso;
    //}
    //public static Vector3 ConvertToIso(Vector2 cart, int z = 0)
    //{
    //    Vector3 iso = new Vector3();
    //    iso = cart.x * Define._tileIsoDir.RightDown + cart.y * Define._tileIsoDir.RightUp;
    //    iso.z = z;
    //    return iso;
    //}
    public static void SetTile(Tilemap[] tilemaps, Vector3Int gridPosition, TileInfo tileInfo)
    {
        for (int i = 0; i < tilemaps.Length; i++)
        {
            tilemaps[i].SetTile(gridPosition, tileInfo.RuleTiles[i]);
        }
    }
}
