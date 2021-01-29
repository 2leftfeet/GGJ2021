using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Station
{
    public float frequency;
    public AudioSource audioSource;
}
public class StationManager : MonoBehaviour
{
    public StationManager instance;
    public float globalNoiseModifier = 1.0f;
    public AudioSource radioNoiseSource;
    public List<Station> stations = new List<Station>();

    [Range(0,100)]
    public float currentFrequency;
    public float stationDecay;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Update()
    {
        float minDistance = float.MaxValue;

        foreach(var station in stations)
        {
            float frequencyDistance = Mathf.Abs(currentFrequency - station.frequency);
            //normalize distance
            frequencyDistance = frequencyDistance > stationDecay ? stationDecay : frequencyDistance;

            float reduction = frequencyDistance/stationDecay;
            station.audioSource.volume = (1.0f - reduction * reduction) * globalNoiseModifier;

            if(minDistance > reduction)
                minDistance = reduction;
        }

        radioNoiseSource.volume = (minDistance * minDistance) * globalNoiseModifier;
    }

    [ContextMenu("Set Frequency To Random Station")]
    public void SetFrequencyToRandomStation()
    {
        currentFrequency = stations[Random.Range(0, stations.Count)].frequency + Random.Range(-0.5f, 0.5f) * stationDecay;
    }
}
