using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot_Content : Imouse
{
    public void GetContent(ref object obj);
    public void UpdateContent();
    public void UpdateContent(object obj);
    public void DeleteContent();
    public bool IsEmpty();
}
