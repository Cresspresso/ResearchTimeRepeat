using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InteractEventComponent))]
[DisallowMultipleComponent]
public abstract class Interactable : MonoBehaviour
{
	public bool showHoverInfo = true;
	public string hoverDescription = "Interact";
	public string hoverNotInteractableDescription = "Can Not Interact";

	public abstract bool IsInteractable(InteractEventArgs eventArgs);
	protected virtual void OnInteract(InteractEventArgs eventArgs) { }

	public InteractEventComponent interactEventComponent { get; private set; }

	protected virtual void Awake()
	{
		interactEventComponent = GetComponent<InteractEventComponent>();
	}

	public void Interact(InteractEventArgs eventArgs)
	{
		Debug.Assert(IsInteractable(eventArgs), "Interactable is being interacted with in an invalid situation.", this);
		OnInteract(eventArgs);
		interactEventComponent.onInteract.Invoke(eventArgs);
	}
}
