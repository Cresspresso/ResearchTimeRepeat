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
	public Rigidbody keyCard;

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

	// Get KeyCard that is closest to where the camera is looking.
	// Author: Elijah
	private IEnumerable<Rigidbody> QueryKeyCards()
	{
		var cameraTransform = this.cameraTransform;
		return
			from pair in availableTouches
			let collider = pair.Key
			where collider && collider.transform && collider.tag == "KeyCard"
			let rb = collider.GetComponentInParent<Rigidbody>()
			where rb && rb != this.keyCard
			let p = collider.transform.position - cameraTransform.position
			let d = Vector3.Distance(p, Vector3.Project(p, cameraTransform.forward))
			orderby d
			select rb;
	}

	private Rigidbody QueryClosestKeyCard()
	{
		return QueryKeyCards().FirstOrDefault();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var selectedKeyCard = QueryClosestKeyCard();
			if (selectedKeyCard)
			{
				Debug.Log($"Trying to pick up {selectedKeyCard.name}");
				// if no obstacles in the way
				var cameraTransform = this.cameraTransform;
				var dir = selectedKeyCard.position - cameraTransform.position;
				if (Physics.Raycast(
					new Ray(cameraTransform.position, dir),
					out var hit,
					dir.magnitude,
					obstacleMask,
					QueryTriggerInteraction.Ignore))
				{
					if (hit.collider.GetComponentInParent<Rigidbody>() == selectedKeyCard)
					{
						PickUp(selectedKeyCard);
					}
					else
					{
						Debug.DrawRay(cameraTransform.position, dir, Color.red, 5.0f);
						Debug.DrawRay(hit.point, Vector3.up, Color.yellow, 5.0f);
						Debug.Log($"Obstacle in the way: {hit.collider.name} {hit.distance} vs {dir.magnitude}", hit.collider);
						Drop();
					}
				}
				else
				{
					PickUp(selectedKeyCard);
				}
			}
			else
			{
				Drop();
			}
		}
	}

	public void Drop()
	{
		if (!keyCard) { return; }
		try
		{
			keyCard.gameObject.layer = LayerMask.NameToLayer("KeyCard");
			keyCard.useGravity = true;
		}
		finally
		{
			this.keyCard = null;
		}
	}

	public void PickUp(Rigidbody keyCard)
	{
		Drop();
		this.keyCard = keyCard;
		keyCard.gameObject.layer = LayerMask.NameToLayer("KeyCardBeingHeld");
		keyCard.useGravity = false;
	}

	private void FixedUpdate()
	{
		if (keyCard)
		{
			keyCard.velocity = Vector3.zero;
			keyCard.angularVelocity = Vector3.zero;
			keyCard.MovePosition(Vector3.Lerp(keyCard.position, handLocation.position, lerpSpeed * Time.deltaTime));
			keyCard.MoveRotation(Quaternion.Slerp(keyCard.rotation, handLocation.rotation, slerpSpeed * Time.deltaTime));
		}
	}
}
