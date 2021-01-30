using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnable : MonoBehaviour
{
    [SerializeField] bool tiggerOnce = true;
    [SerializeField] GameObject toEnable;
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tiggerOnce && !triggered)
            {
                toEnable.SetActive(true);
                triggered = true;
            }
            else
            {
                toEnable.SetActive(true);
            }
        }
    }
}
