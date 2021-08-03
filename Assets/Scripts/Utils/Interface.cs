using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface
{
    public interface ICustomPriorityQueueNode<T>
    {
        public T GetPriority();
    }
}
