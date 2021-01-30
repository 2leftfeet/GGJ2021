using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] PatrolAIController aiToManupulate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                aiToManupulate.Resume();
                triggered = true;
            }
        }
    }
}
