using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlink : MonoBehaviour
{
    [SerializeField] bool DEBUG = false;
    bool isVisible = true;
    [SerializeField] GameObject enemy;
    SkinnedMeshRenderer renderer;
    bool alreadySeen = false;


    private void Start()
    {
        renderer = enemy.GetComponent<SkinnedMeshRenderer>();
    }

    private void LateUpdate()
    {

        if (DEBUG)
        {
            Debug.Log(renderer.isVisible);
        }

        if (!renderer.isVisible && alreadySeen)
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

        if (renderer.isVisible)
        {
            alreadySeen = true;
        }

    }

}
    