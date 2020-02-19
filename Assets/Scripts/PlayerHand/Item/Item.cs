using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemEventComponent))]
public class Item : Interactable
{
	private ItemEventComponent m_itemEventComponent;
	public ItemEventComponent itemEventComponent {
		get
		{
			if (!m_itemEventComponent)
			{
				m_itemEventComponent = GetComponent<ItemEventComponent>();
			}
			return m_itemEventComponent;
		}
	}

	public string hoverHeldDescription = "Item";
	private string hoverOldDescription;

	public PlayerItemHolder holder { get; private set; } = null;

	protected virtual void Awake()
	{
		hoverOldDescription = this.hoverNotInteractableDescription;
	}

	public override NotInteractableReason GetNotInteractableReason(InteractEventArgs eventArgs)
	{
		if (holder)
		{
			return new NotInteractableReason("item is being held");
		}
		else
		{
			return base.GetNotInteractableReason(eventArgs);
		}
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
