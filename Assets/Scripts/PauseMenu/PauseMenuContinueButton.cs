using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuContinueButton : ButtonOnClickSubscriber
{
	public override void OnClick()
	{
		var menu = GetComponentInParent<PauseMenu>();
		if (menu)
		{
			menu.ClosePauseMenu();
		}
		else
		{
			Debug.LogError("PauseMenu is null");
		}
	}
}
