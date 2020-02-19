using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemEventComponent))]
public class Item : Interactable
{
	public string hoverHeldDescription = "Item";
	private string hoverOldDescription;

	public ItemEventComponent itemEventComponent { get; private set; }
	public PlayerItemHolder holder { get; private set; } = null;

	protected override void Awake()
	{
		base.Awake();
		itemEventComponent = GetComponent<ItemEventComponent>();
		hoverOldDescription = this.hoverNotInteractableDescription;
	}

	public override bool IsInteractable(InteractEventArgs eventArgs)
	{
		return !holder;
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		if (holder)
		{
			holder.DropItem();
		}

		holder = eventArgs.hand.GetComponent<PlayerItemHolder>();
		try
		{
			holder.PickUpItem(this);
		}
		catch
		{
			holder = null;
			throw;
		}
	}

	protected virtual void OnPickedUp(ItemEventArgs eventArgs) { }
	protected virtual void OnDropped(ItemEventArgs eventArgs) { }

	public void InvokePickedUp(ItemEventArgs eventArgs)
	{
		holder = eventArgs.holder;
		hoverNotInteractableDescription = hoverHeldDescription;
		OnPickedUp(eventArgs);
		itemEventComponent.onPickedUp.Invoke(eventArgs);
	}

	public void InvokeDropped(ItemEventArgs eventArgs)
	{
		holder = null;
		hoverNotInteractableDescription = hoverOldDescription;
		OnDropped(eventArgs);
		itemEventComponent.onDropped.Invoke(eventArgs);
	}
}
