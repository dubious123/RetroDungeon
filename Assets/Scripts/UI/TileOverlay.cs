using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOverlay
{
    public TileOverlay(Color color)
    {
        _tileColor = color;
    }
    public bool IsActive = false;
    public Dictionary<Vector3Int, Color> TileColorDict = new Dictionary<Vector3Int, Color>();
    Color _tileColor;
    public void Display()
    {
        foreach (KeyValuePair<Vector3Int,Color> pair in TileColorDict)
        {
            Managers.GameMgr.Floor.SetColor(pair.Key, pair.Value);
        }
    }
    public void SetTilePos<T>(T vectors) where T : IEnumerable<Vector3Int>
    {
        TileColorDict.Clear();
        foreach (Vector3Int pos in vectors)
        {
            TileColorDict.Add(pos,_tileColor);
        }
    }
    public void SetTilePos<T>(T vectors, Color color) where T : IEnumerable<Vector3Int>
    {
        TileColorDict.Clear();
        foreach (Vector3Int pos in vectors)
        {
            TileColorDict.Add(pos, color);
        }
    }
    public void AddTilePos<T>(T vectors, Color? color) where T : IEnumerable<Vector3Int>
    {
        if (color.HasValue)
        {
            foreach (Vector3Int pos in vectors)
            {
                if (TileColorDict.ContainsKey(pos)) { TileColorDict[pos] = color.Value; }
                TileColorDict.Add(pos, color.Value);
            }
        }
        else
        {
            foreach (Vector3Int pos in vectors)
            {
                if (TileColorDict.ContainsKey(pos)) { TileColorDict[pos] = _tileColor; }
                TileColorDict.Add(pos, _tileColor);
            }
        }
    }
    public void AddTilePos(Vector3Int pos, Color? color)
    {
        if (color.HasValue)
        {
            if (TileColorDict.ContainsKey(pos)) { TileColorDict[pos] = color.Value; }
            else { TileColorDict.Add(pos, color.Value); }
            
        }
        else
        {
            if (TileColorDict.ContainsKey(pos)) { TileColorDict[pos] = _tileColor; }
            else { TileColorDict.Add(pos, _tileColor); }
            
        }
    }
    public void RemoveTilePos<T>(T vectors) where T : IEnumerable<Vector3Int>
    {
        foreach (Vector3Int pos in vectors)
        {
            TileColorDict.Remove(pos);
        }
    }
    public void RemoveTilePos(Vector3Int pos)
    {
        TileColorDict.Remove(pos);
    }
}
