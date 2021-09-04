using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PerlinNoiseHelper
{
    [Min(1)]
    public static float Scale = 10f;
    public static int OffsetX;
    public static int OffsetY;
    public PerlinNoiseHelper()
    {
        OffsetX = UnityEngine.Random.Range(1, 9999);
        OffsetY = UnityEngine.Random.Range(1, 9999);
    }
    public float GetNoise(int x, int y)
    {
        return Mathf.PerlinNoise(x / Scale + OffsetX, y / Scale + OffsetY);
    }
    public float GetNoise(Vector2 pos)
    {
        return Mathf.PerlinNoise(pos.x / Scale + OffsetX, pos.y / Scale + OffsetY);
    }
    public float GetNoise(Vector3 pos)
    {
        return Mathf.PerlinNoise(pos.x / Scale + OffsetX, pos.y / Scale + OffsetY);
    }
}
