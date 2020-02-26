using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sibling to Interactable
public class SwingDoor : Interactable
{
	public Animator anim;

	public bool isOpen {
		get => anim.GetBool("isOpen");
		set
		{
			anim.SetBool("isOpen", value);
			this.hoverDescription = isLocked
				? "Locked"
				: (value
				? "Close"
				: "Open");

			var am = FindObjectOfType<AudioManager>();
			if (am) { am.PlaySound("doorOpen"); }
		}
	}

	public bool openOnAwake = false;
	public bool isLocked = false;

	protected void Start()
	{
		isOpen = openOnAwake;
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		if (!isLocked)
		{
			isOpen = !isOpen;
		}
	}
}
