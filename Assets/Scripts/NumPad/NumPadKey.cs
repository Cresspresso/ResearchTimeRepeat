using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum NumPadKeyType
{
	Num0 = 0,
	Num1,
	Num2,
	Num3,
	Num4,
	Num5,
	Num6,
	Num7,
	Num8,
	Num9,
	Clear = 10,
	Enter,
}

public sealed class NumPadKey : Interactable
{
	[SerializeField]
	private NumPadKeyType m_type = NumPadKeyType.Num0;
	public NumPadKeyType type => m_type;

	[SerializeField]
	private NumPad m_pad;
	public NumPad pad {
		get
		{
			if (!m_pad)
			{
				m_pad = GetComponentInParent<NumPad>();
			}
			return m_pad;
		}
	}

	public override NotInteractableReason GetNotInteractableReason(InteractEventArgs eventArgs)
	{
		return null;
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		pad.OnNumPadKeyPressed(type);

		var am = FindObjectOfType<AudioManager>();
		if (am) { am.PlaySound("buttonClick"); }
	}
}
