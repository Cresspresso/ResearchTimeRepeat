using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyCardReader))]
public class GroundhogKcr : MonoBehaviour
{
	public KeyCardReader interactable { get; private set; }

	public GroundhogDay groundhogDay;

	private void Awake()
	{
		interactable = GetComponent<KeyCardReader>();

		if (!groundhogDay)
		{
			groundhogDay = FindObjectOfType<GroundhogDay>();
		}
		Debug.Assert(groundhogDay, "groundhogDay is null", this);

		interactable.interactEventComponent.onInteract.AddListener(OnInteract);
	}

	private void OnDestroy()
	{
		interactable.interactEventComponent.onInteract.RemoveListener(OnInteract);
	}

	private void OnInteract(InteractEventArgs eventArgs)
	{
		groundhogDay.RestartTimeLoop();
	}
}
