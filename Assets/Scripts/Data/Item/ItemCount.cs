using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCount : MonoBehaviour
{
    public TextMeshProUGUI Count;
    public void UpdateCount(int num)
    {
        Count.text = num.ToString();
    }
}
