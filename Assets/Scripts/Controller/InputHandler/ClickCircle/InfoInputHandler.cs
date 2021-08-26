using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InfoInputHandler : MonoBehaviour
{
    public UnityEvent InfoEvent;
    private void OnDisable()
    {
        InfoEvent.RemoveAllListeners();
    }
    public void InvokeInfo()
    {
        InfoEvent.Invoke();
    }

}
