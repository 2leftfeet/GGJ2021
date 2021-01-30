using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Station
{
    public float frequency;
    public AudioSource audioSource;
}
public class StationManager : MonoBehaviour
{
    public static StationManager instance;

    [Range(0.0f, 1.0f)]
    public float globalNoiseModifier = 1.0f;
    public float backpackNoiseModifier = 0.3f;
    public AudioSource radioNoiseSource;
    public List<Station> stations = new List<Station>();

    [Range(0,100)]
    public float currentFrequency;
    public float stationDecay;


    bool backpacked = true;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Update()
    {
        float minDistance = float.MaxValue;
        float noiseModifier = backpacked ? backpackNoiseModifier * globalNoiseModifier : globalNoiseModifier;

        foreach(var station in stations)
        {
            float frequencyDistance = Mathf.Abs(currentFrequency - station.frequency);
            //normalize distance
            frequencyDistance = frequencyDistance > stationDecay ? stationDecay : frequencyDistance;

            float reduction = frequencyDistance/stationDecay;
            station.audioSource.volume = (1.0f - reduction * reduction) * noiseModifier;

            if(minDistance > reduction)
                minDistance = reduction;
        }

        radioNoiseSource.volume = (minDistance * minDistance) * noiseModifier;
    }

    [ContextMenu("Set Frequency To Random Station")]
    public void SetFrequencyToRandomStation()
    {
        currentFrequency = stations[Random.Range(0, stations.Count)].frequency + Random.Range(-0.5f, 0.5f) * stationDecay;
    }

     [ContextMenu("Scramble Frequencies")]
    public void ScrambleFrequencies()
    {
        List<float> frequencies = new List<float>();

        foreach(var station in stations)
        {
            frequencies.Add(station.frequency);
        }

        for(int i = 0; i < stations.Count; ++i)
        {
            int index = Random.Range(0, frequencies.Count);
            stations[i].frequency = frequencies[index];
            frequencies.RemoveAt(index);
        }
    }

    public void SetRadioBackpacked(bool value)
    {
        backpacked = value;
    }

    public float ChangeFrequency(float delta)
    {
        currentFrequency += delta;
        if(currentFrequency < 0.0f)
        {
            float change = currentFrequency - delta;
            currentFrequency = 0.0f;
            return change;
        }
        else if(currentFrequency > 100.0f)
        {
            float change = delta - (currentFrequency - 100.0f);
            currentFrequency = 100.0f;
            return change;
        }
        return delta;
    }
}
