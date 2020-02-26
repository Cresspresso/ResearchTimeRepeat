using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sibling to `PlayerHand`
public class PlayerItemHolder : MonoBehaviour
{
	private PlayerHand m_hand;
	public PlayerHand hand {
		get
		{
			if (!m_hand)
			{
				m_hand = GetComponent<PlayerHand>();
			}
			return m_hand;
		}
	}

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
