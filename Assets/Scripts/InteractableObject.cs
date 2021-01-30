using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SharedBool pickUpEvent;
    private bool hovering = false;
    [SerializeField] private MeshRenderer[] renderers;
    [SerializeField] private Color startingColor;
    private float pingPongValue;
    [SerializeField] private bool shouldDestroyOnPickup = false;
    [SerializeField] private SharedBool boolToCheck;
    public void Interact(Transform interactee)
    {
        if (boolToCheck?.Value == false) return;
        if (pickUpEvent != null) pickUpEvent.Value = true;
        if (shouldDestroyOnPickup) Destroy(gameObject);
    }
    private void Update()
    {
        if (boolToCheck?.Value == false) return;

        foreach (var renderer in renderers)
        {
            var color = renderer.material.color;
            if (hovering)
            {
                color = new Color(startingColor.r + Mathf.PingPong(Time.time, 0.5f), startingColor.g +
                    Mathf.PingPong(Time.time, 0.5f), 
                    startingColor.b + Mathf.PingPong(Time.time, 0.5f)) ;
                Debug.Log("Was hovering");
            }
            else
            {
                color = startingColor;
            }
            renderer.material.color = color;
        }

        hovering = false;
    }

    private void Start()
    {
        startingColor = renderers[0].material.color;
    }
    public void Hover()
    {
        Debug.Log("Hovering");
        hovering = true;
    }

    public bool IsHovering() => hovering;
}
