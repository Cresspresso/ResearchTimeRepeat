using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : Interactable
{
	public PlayerHand playerHandHoldingThis { get; private set; } = null;

	public override bool IsInteractable(InteractEventArgs eventArgs)
	{
		return true;
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		try
		{
			playerHandHoldingThis = eventArgs.playerHand;
			eventArgs.playerHand.PickUp(this);
		}
		catch
		{
			playerHandHoldingThis = null;
			throw;
		}
	}
}
