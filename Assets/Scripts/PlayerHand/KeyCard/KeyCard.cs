using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
[RequireComponent(typeof(Rigidbody))]
public class KeyCard : MonoBehaviour
{
	public Item item { get; private set; }
	public Rigidbody rb { get; private set; }
	public KeyCardHoldInfo holdInfo { get; private set; }

	private void Awake()
	{
		item = GetComponent<Item>();
		rb = GetComponent<Rigidbody>();

		item.itemEventComponent.onPickedUp.AddListener(OnPickedUp);
		item.itemEventComponent.onDropped.AddListener(OnDropped);
	}

	private void OnDestroy()
	{
		item.itemEventComponent.onPickedUp.RemoveListener(OnPickedUp);
		item.itemEventComponent.onDropped.RemoveListener(OnDropped);
	}

	private void OnPickedUp(ItemEventArgs eventArgs)
	{
		rb.isKinematic = true;
		holdInfo = eventArgs.holder.GetComponent<KeyCardHoldInfo>();
		transform.SetParent(holdInfo.location);
		transform.localPosition = Vector3.zero;
	}

	private void OnDropped(ItemEventArgs eventArgs)
	{
		rb.isKinematic = false;
		holdInfo = null;
		transform.SetParent(null);
	}
}
