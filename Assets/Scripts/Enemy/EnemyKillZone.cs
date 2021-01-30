using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    private void Start()
    {
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("KILL");
        }
    }
}
