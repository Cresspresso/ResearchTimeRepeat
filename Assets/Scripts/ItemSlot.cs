using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemSlot
{
	[SerializeField]
	private ItemTypeObject m_type;
	public ItemTypeObject type => m_type;
	[SerializeField]
	private int m_amount;
	public int amount => m_amount;

	public bool isEmpty => !m_type;

	public ItemSlot(ItemTypeObject type, int amount)
	{
		m_type = type;
		m_amount = amount;
	}

	public void EditorValidate()
	{
		if (!m_type) { m_amount = 0; }
		else if (m_amount < 1) { m_amount = 1; }
	}

	public void Clear()
	{
		m_type = null;
		m_amount = 0;
	}

	public ItemSlot TakeOwnership()
	{
		var copy = this;
		Clear();
		return copy;
	}
}
