using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    public SharedBool OnInteractEvent;
    public Transform positionToMove;

    [SerializeField] private SharedBool boolToCheck;
    
    public TMP_Text text;
    public bool hovering = false;

    private Transform interactee;

    private bool opening;
    private void Update()
    {
        if (boolToCheck?.Value == false) return;
        
        switch (text.gameObject.activeInHierarchy)
        {
            case false when hovering:
                text.gameObject.SetActive(true);
                break;
            case true when !hovering:
                text.gameObject.SetActive(false);
                break;
        }

        hovering = false;
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
    }

    public void Hover()
    {
        hovering = true;
    }
    public void Interact(Transform interactee)
    {
        if (opening || boolToCheck?.Value == false) return;
        StartCoroutine(Fade());
        this.interactee = interactee;
    }

    public bool IsHovering() => hovering;
    private IEnumerator Fade()
    {
        opening = true;
        
        var obj = new GameObject();
        var canvas = obj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //canvas.worldCamera = Camera.main;
        canvas.sortingOrder = 50;
        var image = obj.AddComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0f);
        
        for (float i = 0; i < 1; i+=0.01f)
        {
            image.color =  new Color(0f, 0f, 0f, i);
            yield return null;
        }

        interactee.position = positionToMove.position;
        if(OnInteractEvent!=null) OnInteractEvent.Value = true;
        
        for (float i = 1; i > 0; i-=0.01f)
        {
            image.color =  new Color(0f, 0f, 0f, i);
            yield return null;
        }

        opening = false;
        Destroy(obj);
    }
    
}