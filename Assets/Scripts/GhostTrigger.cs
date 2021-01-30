using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public Transform ghost;
    GhostLerp gl;
    // Start is called before the first frame update
    void Start()
    {
        gl = ghost.GetComponent<GhostLerp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gl.StartLerp();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
