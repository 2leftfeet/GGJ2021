using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightExplosion : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] float timer;

    public bool isRandomised = true;

    private void Start()
    {
        RandomiseTimers();
        InvokeRepeating("TriggerExplosion", startTime, timer);
    }

    void TriggerExplosion()
    {
        RandomiseTimers();
        GetComponent<Animator>().SetTrigger("Boom");
    }

    void RandomiseTimers()
    {
        if (isRandomised)
        {
            startTime = Random.Range(1.0f, 5.0f);
            timer = Random.Range(6f, 10f);
        }
    }
}
