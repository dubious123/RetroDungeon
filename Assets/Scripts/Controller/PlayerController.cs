using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    MouseInput mouseInput;
    Camera mainCamara;
    public Tilemap map;
    float _movementSpeed;
    private Vector3 destination;
    private void Awake()
    {
        mouseInput = new MouseInput();
        mainCamara = Camera.main;
    }
    private void OnEnable()
    {
        mouseInput.Enable();
    }
    private void OnDisable()
    {
        mouseInput.Disable();
    }
    
    private void Start()
    {
        destination = transform.position;
        _movementSpeed = Managers.Game.PlayerData.Movespeed;
        map = Managers.Game.Tilemaps[0].GetComponent<Tilemap>();
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }
    private void MouseClick()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = mainCamara.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        Debug.Log(gridPosition);
        if (map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }
    }
    public void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 0.1f) 
        transform.position = Vector3.MoveTowards(transform.position, destination, _movementSpeed * Time.deltaTime);
    }

}
