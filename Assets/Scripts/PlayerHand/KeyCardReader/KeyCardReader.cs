using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class KeyCardReader : Interactable
{
	public override bool IsInteractable(InteractEventArgs eventArgs)
	{
		var holder = eventArgs.hand.GetComponent<PlayerItemHolder>();
		var keyCard = holder.itemBeingHeld.GetComponent<KeyCard>();
		return (bool)keyCard;
	}
}
