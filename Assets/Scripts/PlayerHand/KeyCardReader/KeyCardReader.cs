using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class KeyCardReader : Interactable
{
	public override bool IsInteractable(InteractEventArgs eventArgs)
	{
		var holder = eventArgs.hand.GetComponent<PlayerItemHolder>();

		var item = holder.itemBeingHeld;
		if (!item) { return false; }

		var keyCard = item.GetComponent<KeyCard>();
		return (bool)keyCard;
	}
}
