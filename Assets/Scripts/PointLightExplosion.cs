using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightExplosion : MonoBehaviour
{

    //float randomTimer = Random.Range(6f, 10f);

    private void Start()
    {
        InvokeRepeating("TriggerExplosion", 2.0f, 10f);
    }

    void TriggerExplosion()
    {
        GetComponent<Animator>().SetTrigger("Boom");
    }
}
