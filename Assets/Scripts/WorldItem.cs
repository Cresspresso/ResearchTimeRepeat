using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
	public ItemSlot itemSlot;

	void OnValidate()
	{
		itemSlot.EditorValidate();
	}

	void Start()
	{
		Debug.Assert(!itemSlot.isEmpty, "WorldItem must have at least one item", this);
	}
}
