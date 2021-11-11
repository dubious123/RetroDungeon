using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public abstract ISlot_Content GetContent();
    public abstract void UpdateContent(ISlot_Content content);
    public abstract void DeleteContent();
    public abstract bool IsEmpty();
}
