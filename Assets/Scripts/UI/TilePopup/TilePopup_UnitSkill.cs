using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePopup_UnitSkill : MonoBehaviour
{
    [SerializeField] GameObject _None;
    [SerializeField] GameObject _SkillBox;
    [SerializeField] RectTransform _Self;
    List<GameObject> childBoxes = new List<GameObject>();
    public void Init(BaseUnitData data)
    {
       
        childBoxes = new List<GameObject>();
        if(data.SkillDict.Count == 0) 
        {
            _None.SetActive(true);
            return; 
        }
        else { _None.SetActive(false); }
        foreach (KeyValuePair<string, SkillLibrary.BaseSkill> pair in data.SkillDict)
        {
            GameObject skillBox = Managers.ResourceMgr.Instantiate(_SkillBox, transform);
            skillBox.GetComponent<TextMeshProUGUI>().text = pair.Key;
            childBoxes.Add(skillBox);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_Self);
    }
    public void OnClose()
    {
        foreach(GameObject go in childBoxes)
        {
            Managers.ResourceMgr.Destroy(go);
        }
    }
}
