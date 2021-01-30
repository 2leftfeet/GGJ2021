using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlink : MonoBehaviour
{
    bool DEBUG = false;
    bool isVisible = true;
    [SerializeField] GameObject enemy;
    Renderer renderer;


    private void Start()
    {
        renderer = enemy.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (DEBUG)
        {
            Debug.Log(renderer.isVisible);
        }

        if (!renderer.isVisible)
        {
            enemy.SetActive(false);
        }

        if (DEBUG)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                enemy.SetActive(true);
            }
        }
    }

}
    