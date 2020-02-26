using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyCardReader))]
public class DoorOpenerKcr : MonoBehaviour
{
	public KeyCardReader interactable { get; private set; }
	public Animator anim;

	private void Awake()
	{
		interactable = GetComponent<KeyCardReader>();

		interactable.interactEventComponent.onInteract.AddListener(OnInteract);
	}

	private void OnDestroy()
	{
		interactable.interactEventComponent.onInteract.RemoveListener(OnInteract);
	}

	private void OnInteract(InteractEventArgs eventArgs)
	{
		anim.SetBool("doorOpen", true);

		var am = FindObjectOfType<AudioManager>();
		if (am) { am.PlaySound("labdoorOpen"); }
	}
}
