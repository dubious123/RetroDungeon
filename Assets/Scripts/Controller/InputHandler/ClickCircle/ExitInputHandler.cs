using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitInputHandler : MonoBehaviour
{
    public UnityEvent ExitEvent;
    private void OnDisable()
    {
        ExitEvent.RemoveAllListeners();
    }
    public void InvokeExit()
    {
        ExitEvent.Invoke();
    }
}
