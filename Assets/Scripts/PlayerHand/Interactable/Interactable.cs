using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotInteractableReason
{
	public string reason { get; set; }

	public NotInteractableReason(string reason)
	{
		this.reason = reason;
	}
}

[RequireComponent(typeof(InteractEventComponent))]
[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
	public bool showHoverInfo = true;
	public string hoverDescription = "Interact";
	public string hoverNotInteractableDescription = "Can Not Interact";

	/// <returns><see langword="null"/> if this object can be interacted with.</returns>
	public virtual NotInteractableReason GetNotInteractableReason(InteractEventArgs eventArgs)
	{
		return null;
	}

	private InteractEventComponent m_interactEventComponent;
	public InteractEventComponent interactEventComponent {
		get
		{
			if (!m_interactEventComponent)
			{
				m_interactEventComponent = GetComponent<InteractEventComponent>();
			}
			return m_interactEventComponent;
		}
	}

	protected virtual void OnInteract(InteractEventArgs eventArgs) { }

	public void Interact(InteractEventArgs eventArgs)
	{
#if UNITY_EDITOR
		var nir = GetNotInteractableReason(eventArgs);
		if (nir != null)
		{
			Debug.LogError("Interactable is being interacted with in an invalid situation. Reason: " + nir.reason, this);
		}
#endif // UNITY_EDITOR

		OnInteract(eventArgs);
		interactEventComponent.onInteract.Invoke(eventArgs);
	}
}
