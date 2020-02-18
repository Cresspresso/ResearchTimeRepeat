using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

// sibling to trigger colliders
public class PlayerHand : MonoBehaviour
{
	public LayerMask obstacleMask = ~0;
	public Transform handLocation;
	public float lerpSpeed = 5.0f;
	public float slerpSpeed = 5.0f;

	[SerializeField]
	private UnityEvent m_onNothingToInteract = new UnityEvent();
	public UnityEvent onNothingToInteract => m_onNothingToInteract;

	public Transform cameraTransform => Camera.main.transform;

	private Dictionary<Collider, int> availableTouches = new Dictionary<Collider, int>();

#if UNITY_EDITOR
	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 200, 60), $"count: {availableTouches.Count}");
	}
#endif

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
	private IEnumerable<Interactable> QueryInteractables(InteractEventArgs eventArgs)
	{
		var cameraTransform = this.cameraTransform;
		return
			from pair in availableTouches
			let collider = pair.Key
			where collider && collider.transform
			let item = collider.GetComponentInParent<Interactable>()
			where item && item.IsInteractable(eventArgs)
			let p = collider.transform.position - cameraTransform.position
			let d = Vector3.Distance(p, Vector3.Project(p, cameraTransform.forward))
			orderby d
			select item;
	}

	private Interactable QueryClosestInteractable(InteractEventArgs eventArgs)
	{
		return QueryInteractables(eventArgs).FirstOrDefault();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var eventArgs = new InteractEventArgs(hand: this);
			var itemInQuestion = QueryClosestInteractable(eventArgs);
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
						Debug.Log($"Interacting with {itemOnTop.name}", itemOnTop);
						itemOnTop.Interact(eventArgs);
					}
					else
					{
						onNothingToInteract.Invoke();

						Debug.DrawRay(cameraTransform.position, dir, Color.red, 5.0f);
						Debug.DrawRay(hit.point, Vector3.up, Color.yellow, 5.0f);
						Debug.Log($"Obstacle in the way: {hit.collider.name} {hit.distance} vs {dir.magnitude}", hit.collider);
					}
				}
				else
				{
					Debug.Log($"Interacting with {itemInQuestion.name}", itemInQuestion);
					itemInQuestion.Interact(eventArgs);
				}
			}
			else
			{
				Debug.Log("Nothing to interact with.", this);
				onNothingToInteract.Invoke();
			}
		}
	}
}
