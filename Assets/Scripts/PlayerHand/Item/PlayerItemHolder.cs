using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sibling to `PlayerHand`
public class PlayerItemHolder : MonoBehaviour
{
	public Item itemBeingHeld { get; private set; } = null;

	private void Update()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			DropItem();
		}
	}

	public Item DropItem()
	{
		if (!itemBeingHeld)
		{
			return itemBeingHeld;
		}

		var itemBeingDropped = itemBeingHeld;
		this.itemBeingHeld = null;

		itemBeingDropped.InvokeDropped(new ItemEventArgs(holder: this));
		return itemBeingDropped;
	}

	public void PickUpItem(Item item)
	{
		DropItem();
		this.itemBeingHeld = item;
		item.InvokePickedUp(new ItemEventArgs(holder: this));
	}
}
