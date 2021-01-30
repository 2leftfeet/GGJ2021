using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SharedBool pickUpEvent;
    private bool hovering = false;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Color startingColor;
    private float pingPongValue;
    [SerializeField] private bool shouldDestroyOnPickup = false;
    public void Interact(Transform interactee)
    {
        if(pickUpEvent != null) pickUpEvent.Value = true;
        if (shouldDestroyOnPickup) Destroy(this);
    }
    private void Update()
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
        
        hovering = false;
    }

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        startingColor = renderer.material.color;
    }
    public void Hover()
    {
        Debug.Log("Hovering");
        hovering = true;
    }

    public bool IsHovering() => hovering;
}
