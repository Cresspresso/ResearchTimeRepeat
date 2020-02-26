using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelButton : Interactable
{
	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		Debug.Log("Interacted!", this);
		base.OnInteract(eventArgs);
	}
}
