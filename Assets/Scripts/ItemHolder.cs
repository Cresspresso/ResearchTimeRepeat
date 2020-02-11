using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviour
{
	public const int NumItems = 10;
	public HoldableItem[] items = new HoldableItem[NumItems];
	[Range(0, NumItems - 1)]
	public int selectedIndex = 0;

	public class TransferredEventArgs
	{
		public ItemHolder from { get; private set; }
		public ItemHolder to { get; private set; }
		public HoldableItem item { get; private set; }

		public TransferredEventArgs(ItemHolder from, ItemHolder to, HoldableItem item)
		{
			this.from = from;
			this.to = to;
			this.item = item;
		}
	}

	[System.Serializable]
	public class TransferredEvent : UnityEvent<TransferredEventArgs> { }

	[HideInInspector]
	public TransferredEvent onItemTransferred = new TransferredEvent();



	private static int FindIndex<T>(IList<T> list, System.Predicate<T> predicate)
	{
		for (int i = 0; i < list.Count; ++i)
		{
			if (predicate(list[i])) { return i; }
		}
		return -1;
	}

	private void ReceiveItem(ItemHolder from, HoldableItem item, int index)
	{
		items[index] = item;
		item.holder = this;
		Utils.TryExceptLog(this, () => onItemTransferred.Invoke(new TransferredEventArgs(
			from: from,
			to: this,
			item: item
		)));
	}

	public bool TryReceiveItem(ItemHolder from, HoldableItem item)
	{
		var i = FindIndex(items, x => x == null);
		if (i == -1)
		{
			return false;
		}
		else
		{
			ReceiveItem(from, item, i);
			return true;
		}
	}
}
