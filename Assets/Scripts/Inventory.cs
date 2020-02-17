using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public ItemSlot[] hotbarSlots = new ItemSlot[10];
	
	void OnValidate()
	{
		for (int i = 0; i < hotbarSlots.Length; i++)
		{
			hotbarSlots[i].EditorValidate();
		}
	}
}
