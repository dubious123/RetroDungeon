using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData
{
    float _moveSpeed;
    public float Movespeed { get { return _moveSpeed; } }
    public PlayerData(GameObject player,Transform parent)
    {
        Init(player,parent);
    }
    void Init(GameObject player, Transform parent)
    {
        player.transform.position = parent.transform.position;
        player.transform.position += new Vector3(0f, -0.1f, 0f);
        _moveSpeed = 1.0f;
    }
}
