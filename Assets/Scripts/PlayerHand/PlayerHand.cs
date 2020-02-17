using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHand : MonoBehaviour
{
	public LayerMask obstacleMask = ~0;
	public Transform handLocation;
	public float lerpSpeed = 5.0f;
	public float slerpSpeed = 5.0f;
	public Interactable itemBeingHeld { get; private set; } = null;

	public const string itemLayerName = "KeyCard";
	public const string itemBeingHeldLayerName = "KeyCardBeingHeld";

	public Transform cameraTransform => Camera.main.transform;

	private Dictionary<Collider, int> availableTouches = new Dictionary<Collider, int>();

	private void OnTriggerEnter(Collider other)
	{
		if (availableTouches.ContainsKey(other))
		{
			++availableTouches[other];
		}
		else
		{
			availableTouches.Add(other, 1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (availableTouches.ContainsKey(other))
		{
			--availableTouches[other];
			if (availableTouches[other] <= 0)
			{
				availableTouches.Remove(other);
			}
		}
	}

	// Get item that is closest to where the camera is looking.
	// Author: Elijah
	private IEnumerable<Interactable> QueryItems()
	{
		var cameraTransform = this.cameraTransform;
		return
			from pair in availableTouches
			let collider = pair.Key
			where collider && collider.transform && collider.tag == itemLayerName
			let item = collider.GetComponentInParent<Interactable>()
			where item && item != this.itemBeingHeld
			let p = collider.transform.position - cameraTransform.position
			let d = Vector3.Distance(p, Vector3.Project(p, cameraTransform.forward))
			orderby d
			select item;
	}

	private Interactable QueryClosestItem()
	{
		return QueryItems().FirstOrDefault();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var itemInQuestion = QueryClosestItem();
			if (itemInQuestion)
			{
				Debug.Log($"Trying to pick up {itemInQuestion.name}");

				// if no obstacles in the way
				var cameraTransform = this.cameraTransform;
				var dir = itemInQuestion.transform.position - cameraTransform.position;
				if (Physics.Raycast(
					new Ray(cameraTransform.position, dir),
					out var hit,
					dir.magnitude,
					obstacleMask,
					QueryTriggerInteraction.Ignore))
				{
					var itemOnTop = hit.collider.GetComponentInParent<Interactable>();
					if (itemOnTop)
					{
						InteractWith(itemOnTop);
					}
					else
					{
						Drop();

						Debug.DrawRay(cameraTransform.position, dir, Color.red, 5.0f);
						Debug.DrawRay(hit.point, Vector3.up, Color.yellow, 5.0f);
						Debug.Log($"Obstacle in the way: {hit.collider.name} {hit.distance} vs {dir.magnitude}", hit.collider);
					}
				}
				else
				{
					InteractWith(itemInQuestion);
				}
			}
			else
			{
				Drop();
			}
		}
	}

	// called from non-item maybe-interactable classes
	public void InteractWith(Interactable item)
	{
		Debug.Log($"Interacting with {item.name}");

		item.Interact(new InteractEventArgs { playerHand = this });
	}

	// called from item classes
	public void Drop()
	{
		if (!itemBeingHeld) { return; }
		try
		{
			itemBeingHeld.gameObject.layer = LayerMask.NameToLayer(itemLayerName);
		}
		finally
		{
			this.itemBeingHeld = null;
		}
	}

	// called from item classes
	public void PickUp(Interactable item)
	{
		Drop();
		this.itemBeingHeld = item;
		item.gameObject.layer = LayerMask.NameToLayer(itemBeingHeldLayerName);
	}
}
