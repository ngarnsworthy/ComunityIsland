using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class CallbackList<T> : List<T>
{
    public delegate void Del(T changedValue);

    public Del callback;

    public new T this[int index]
    {
        get
        {
            return base[index];
        }
        set
        {
            callback(value);
            base[index] = value;
        }
    }

    public void Add(T newItem){
        base.Add(newItem);
        callback(newItem);
    }
}
