using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public SharedBool testEvent;

    private void Start()
    {
        testEvent.valueChangeEvent.AddListener(DoSomething);
    }

    public void DoSomething(bool kazkas)
    {
        Debug.Log(testEvent.Value);
        Debug.Log("Pempis" + kazkas);
    }

    private void OnDisable()
    {
        testEvent.valueChangeEvent.RemoveListener(DoSomething);
    }
}
