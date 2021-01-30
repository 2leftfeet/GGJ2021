using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 3f;
    [SerializeField] private SharedVector3 playerPosition;
    [SerializeField] private float minimumHearValue;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Start()
    {
        playerPosition.valueChangeEvent.AddListener(UpdateSound);
    }

    private void UpdateSound()
    {
        if ((transform.position - playerPosition.Value ).magnitude < detectionRadius)
        {
            StationManager.instance.globalNoiseModifier = 1f;
            return;
        }

        StationManager.instance.globalNoiseModifier = 1 - (transform.position - playerPosition.Value).magnitude / minimumHearValue;
    }
    private void OnDestroy()
    {
        playerPosition.valueChangeEvent.RemoveListener(UpdateSound);
    }
}
