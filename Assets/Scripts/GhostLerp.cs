using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLerp : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    public Transform ghost;
    AudioSource ghostAS;

    // Movement speed in units per second.
    public float speed = 1.0F;
    private float startSpeed = 0.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        startSpeed = speed;
        speed = 0f;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        ghostAS = ghost.GetComponent<AudioSource>();
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        ghost.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

        if (ghost.position == endMarker.position)
        {
            ghostAS.Stop();
        }
    }

    public void StartLerp()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        ghostAS.Play();
        speed = startSpeed;
    }
}
