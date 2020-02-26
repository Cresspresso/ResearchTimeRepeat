using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sibling to `Interactable`
public class DoubleDoorInteractive : MonoBehaviour
{
	private Interactable m_interactive;
	public Interactable interactive {
		get
		{
			if (!m_interactive)
			{
				m_interactive = GetComponent<Interactable>();
			}
			return m_interactive;
		}
	}

	public DoubleDoor doubleDoor;

	public bool hasBeenInteracted = false;

	public string descriptionWhenOpened = "Door has been opened";

	private void Awake()
	{
		interactive.interactEventComponent.onInteract.AddListener(OnInteract);
	}

	private void OnDestroy()
	{
		interactive.interactEventComponent.onInteract.RemoveListener(OnInteract);
	}

	private void OnInteract(InteractEventArgs eventArgs)
	{
		if (hasBeenInteracted) { return; }

		hasBeenInteracted = true;

		interactive.hoverDescription = descriptionWhenOpened;

		doubleDoor.OpenDoors();
	}
}
