using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedBool : ScriptableObject
{
    [Header("Set only for debug purposes")]
    public bool _value;
    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            valueChangeEvent.Invoke(_value);
        }
    }
    public UnityEvent<bool> valueChangeEvent = new UnityEvent<bool>();
    public static implicit operator bool(SharedBool value) { return value.Value; }

}
