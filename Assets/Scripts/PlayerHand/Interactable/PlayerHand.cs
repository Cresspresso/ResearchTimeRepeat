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

	[SerializeField]
	private UnityEvent m_onNothingToInteract = new UnityEvent();
	public UnityEvent onNothingToInteract => m_onNothingToInteract;

	public Transform cameraTransform => Camera.main.transform;

	private Dictionary<Collider, int> availableTouches = new Dictionary<Collider, int>();

	public List<Interactable> interactablesBeingHovered { get; private set; } = new List<Interactable>();

	private InteractEventArgs eventArgs;
	public NotInteractableReason GetNotInteractableReason(Interactable interactable)
	{
		// if no obstacles in the way
		var cameraTransform = this.cameraTransform;
		var dir = interactable.transform.position - cameraTransform.position;
		if (Physics.Raycast(
			new Ray(cameraTransform.position, dir),
			out var hit,
			dir.magnitude,
			obstacleMask,
			QueryTriggerInteraction.Ignore))
		{
			var objectOnTop = hit.collider.GetComponentInParent<Interactable>();
			if (objectOnTop != interactable)
			{
				var oName = objectOnTop ? objectOnTop.name : hit.collider.name;
				return new NotInteractableReason("something is in the way: " + oName);
			}
		}

		return interactable.GetNotInteractableReason(eventArgs);
	}

	public Interactable GetClosestHovered() => interactablesBeingHovered.FirstOrDefault();
	public Interactable GetClosestInteractable() => interactablesBeingHovered.FirstOrDefault(i => GetNotInteractableReason(i) == null);



	private void Awake()
	{
		eventArgs = new InteractEventArgs(this);
	}

	private void OnDestroy()
	{
		eventArgs = null;
	}

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
	private IEnumerable<Interactable> QueryInteractables()
	{
		var cameraTransform = this.cameraTransform;
		return
			from pair in availableTouches
			let collider = pair.Key
			where collider && collider.transform
			let item = collider.GetComponentInParent<Interactable>()
			where item
			let p = collider.transform.position - cameraTransform.position
			let d = Vector3.Distance(p, Vector3.Project(p, cameraTransform.forward))
			orderby d
			select item;
	}

	private void Update()
	{
		interactablesBeingHovered = QueryInteractables().ToList();
		/*Debug.Log(interactablesBeingHovered.Take(5).Aggregate("", (sum, b) =>
		{
			sum += ", ";
			sum += b.name;
			var re = GetNotInteractableReason(b);
			if (re != null)
			{
				sum += ":";
				sum += re.reason;
			}
			return sum;
		}), this);*/

		if (Input.GetButtonDown("Fire1"))
		{
			var interactable = GetClosestInteractable();
			if (interactable)
			{
				interactable.Interact(eventArgs);
			}
			else
			{
				onNothingToInteract.Invoke();
			}
		}
	}
}
