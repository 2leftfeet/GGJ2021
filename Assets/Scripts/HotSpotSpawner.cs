using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aura2API;
using JetBrains.Annotations;
using UnityEngine;

public class HotSpotSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private GameObject HotSpotGameObject;
    [SerializeField] [CanBeNull] private GameObject currentSpawnPoint;

    [SerializeField] private SharedBool[] RespawnHotSpotEvents;
    
    private void Start()
    {
        foreach (var evnt in RespawnHotSpotEvents)
        {
            evnt.valueChangeEvent.AddListener(Spawn);
        }
        
        Spawn(true);
    }
    
    private void Spawn(bool value)
    {
        if (!value) return;
        if(currentSpawnPoint != null) Destroy(currentSpawnPoint);

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No Spawn Points, ignoring...");
            return;
        }
        
        var tr = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform;
        var obj = Instantiate(HotSpotGameObject, tr.position, Quaternion.identity);
        obj.transform.localPosition = Vector3.zero;
        
    }

    private void OnDestroy()
    {
        foreach (var evnt in RespawnHotSpotEvents)
        {
            evnt.valueChangeEvent.RemoveListener(Spawn);
        }
    }
}
