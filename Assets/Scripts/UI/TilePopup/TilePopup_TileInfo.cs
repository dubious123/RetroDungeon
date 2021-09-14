using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TilePopup_TileInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _TileBox;
    public void Init(TileInfo info)
    {
        _TileBox.text = "TileType : " + info.Type.ToString();
    }
}
