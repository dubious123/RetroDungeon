using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePopup_UnitDetail : MonoBehaviour
{
    [SerializeField] Button _Btn;
    [SerializeField] TextMeshProUGUI _Ap;
    [SerializeField] TextMeshProUGUI _EyeSight;
    [SerializeField] TextMeshProUGUI _EnemyList;
    [SerializeField] TextMeshProUGUI _AllyList;
    public void ShowAll()
    {
        foreach (GameObject go in gameObject.GetChildren<GameObject>())
        {
            go.SetActive(true);
        }
        _Btn.onClick.RemoveListener(ShowAll);
        _Btn.onClick.AddListener(HideAll);
    }
    private void HideAll()
    {
        foreach(GameObject go in gameObject.GetChildren<GameObject>())
        {
            go.SetActive(false);
        }
        _Btn.onClick.RemoveListener(HideAll);
        _Btn.onClick.AddListener(ShowAll);
    }
     
    public void Init(BaseUnitData data)
    {
        _Ap.text = $"Max Ap : {data.MaxAp}  Current Ap : {data.CurrentAp}  Recover Ap : {data.RecoverAp}";
        _EyeSight.text = $"Eye Sight : {data.EyeSight}";
        if(data.EnemyList.Count == 0) { _EnemyList.text = "Enemy List : empty"; }
        else { _EnemyList.text = $"Enemy List : {data.EnemyList.Aggregate((x, y) => x + ", " + y)}"; }
        if(data.AllienceList.Count == 0) { _AllyList.text = "Ally List : empty"; }
        else { _AllyList.text = $"Ally List : {data.AllienceList.Aggregate((x, y) => x + ", " + y)}"; }
        
    }
}
