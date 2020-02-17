using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyCardReader : Interactable
{
	public override bool IsInteractable(InteractEventArgs eventArgs)
	{
		return eventArgs.playerHand.itemBeingHeld is KeyCard;
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{ }
}
