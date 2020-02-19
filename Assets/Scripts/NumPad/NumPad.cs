using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Text;

public class NumPad : MonoBehaviour
{
	public int maxLength = 4;
	public bool submitAtMaxLength = true;

	private StringBuilder stringBuilder = new StringBuilder();
	public string code { get; private set; }

	[System.Serializable]
	public class CodeEvent : UnityEvent<string> { }
	[SerializeField]
	private CodeEvent m_onCodeChanged = new CodeEvent();
	public CodeEvent onCodeChanged => m_onCodeChanged;
	[SerializeField]
	private CodeEvent m_onSubmit = new CodeEvent();
	public CodeEvent onSubmit => m_onSubmit;



	private void Start()
	{
		InvokeCodeChanged();
	}

	public static char GetChar(NumPadKeyType type)
	{
		if (type == NumPadKeyType.Enter) { return ' '; }
		else if (type == NumPadKeyType.Clear) { return '*'; }
		else { return (char)((int)'0' + (int)type); }
	}

	private void InvokeCodeChanged()
	{
		code = stringBuilder.ToString();
		onCodeChanged.Invoke(code);
	}

	private void Submit()
	{
		onSubmit.Invoke(code);
	}

	public void OnNumPadKeyPressed(NumPadKeyType type)
	{
		if (type == NumPadKeyType.Enter)
		{
			Submit();
		}
		else if (type == NumPadKeyType.Clear)
		{
			// clear
			stringBuilder.Clear();
			InvokeCodeChanged();
		}
		else
		{
			// digit numeral

			if (stringBuilder.Length != maxLength)
			{
				stringBuilder.Append(GetChar(type));
				InvokeCodeChanged();

				if (submitAtMaxLength && stringBuilder.Length == maxLength)
				{
					Submit();
				}
			}
		}
	}
}
