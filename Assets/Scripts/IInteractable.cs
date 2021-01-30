using UnityEngine;

public interface IInteractable
{

    public void Interact(Transform interactee);
    public void Hover();
    public bool IsHovering();
}