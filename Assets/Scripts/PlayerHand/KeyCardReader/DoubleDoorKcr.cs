using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorKcr : MonoBehaviour
{
	private KeyCardReader m_interactive;
	public KeyCardReader interactive {
		get
		{
			if (!m_interactive)
			{
				m_interactive = GetComponent<KeyCardReader>();
			}
			return m_interactive;
		}
	}
	public Animator[] anims = new Animator[1];
	public bool hasBeenInteracted = false;

	public string descriptionWhenOpened = "Door has been opened";

	private void Awake()
	{
		interactive.interactEventComponent.onInteract.AddListener(OnInteract);
	}

	private void OnDestroy()
	{
		interactive.interactEventComponent.onInteract.RemoveListener(OnInteract);
	}

	private void OnInteract(InteractEventArgs eventArgs)
	{
		if (hasBeenInteracted) { return; }

		hasBeenInteracted = true;

		interactive.hoverDescription = descriptionWhenOpened;

		if (anims != null)
		{
			foreach (var anim in anims)
			{
				if (anim)
				{
					anim.SetBool("doorOpen", true);
				}
				else
				{
					Debug.LogError("animator is null", this);
				}
			}
		}
	}
}
