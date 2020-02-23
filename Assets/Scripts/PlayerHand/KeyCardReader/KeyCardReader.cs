using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class KeyCardReader : Interactable
{
	public override NotInteractableReason GetNotInteractableReason(InteractEventArgs eventArgs)
	{
		var holder = eventArgs.hand.GetComponent<PlayerItemHolder>();

		var item = holder.itemBeingHeld;
		if (!item) { return new NotInteractableReason("player is not holding an item"); }

		var keyCard = item.GetComponent<KeyCard>();
		if (!keyCard) { return new NotInteractableReason("player is not holding a KeyCard"); }

		return base.GetNotInteractableReason(eventArgs);
	}
}
