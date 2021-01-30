using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayer : MonoBehaviour
{
    [SerializeField] bool debug = false; 
    //float frequencyCheceked = 0.1f;
    float spotDurationToDetect = 1f;
    float viewrange = 10f;
    [SerializeField] Transform offset;
    float detectionsInARow = 0;
    float requiredForDetections;

    // Start is called before the first frame update
    void Start()
    {
        requiredForDetections = spotDurationToDetect / Time.fixedDeltaTime;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (ScanAreaForPlayer())
        {
            detectionsInARow++;
            if (debug) Debug.Log("PlayerHit amount: " + detectionsInARow);
        }
        else
        {
            detectionsInARow = 0;
        }


        if (detectionsInARow > requiredForDetections)
        {
            if(debug) Debug.Log("SPOTTED");
        }
    }


    bool ScanAreaForPlayer()
    {
        int RaysToShoot = 30;
        float angle = 0;
        Vector3 lookdirection = transform.position + (transform.forward * viewrange);

        for (int i = 0; i < RaysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            float z = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;



            Vector3 dir = new Vector3(lookdirection.x + x, lookdirection.y + y, lookdirection.z + z);
            RaycastHit hit;
            if (debug) Debug.DrawLine(offset.position, dir, Color.red);
            if (Physics.Raycast(offset.position, dir, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
