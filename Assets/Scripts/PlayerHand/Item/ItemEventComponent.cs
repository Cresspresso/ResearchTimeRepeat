using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEventArgs
{
	public PlayerItemHolder holder;

	public ItemEventArgs(PlayerItemHolder holder)
	{
		this.holder = holder;
	}
}

[DisallowMultipleComponent]
public sealed class ItemEventComponent : MonoBehaviour
{
	[System.Serializable]
	public class ItemEvent : UnityEvent<ItemEventArgs> { }
	[SerializeField]
	private ItemEvent m_onDropped = new ItemEvent();
	public ItemEvent onDropped => m_onDropped;
	[SerializeField]
	private ItemEvent m_onPickedUp = new ItemEvent();
	public ItemEvent onPickedUp => m_onPickedUp;
}
