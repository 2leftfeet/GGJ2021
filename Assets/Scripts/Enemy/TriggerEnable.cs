using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnable : MonoBehaviour
{
    [SerializeField] bool tiggerOnce = true;
    [SerializeField] MonoBehaviour toEnable;
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tiggerOnce && !triggered)
            {
                toEnable.enabled = true;
                triggered = true;
            }
            else
            {
                toEnable.enabled = true;
            }
        }
    }
}
