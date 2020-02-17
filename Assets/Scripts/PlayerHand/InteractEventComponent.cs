using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class InteractEventComponent : MonoBehaviour
{
	[System.Serializable]
	public class InteractEvent : UnityEvent<InteractEventArgs> { }
	[SerializeField]
	private InteractEvent m_onInteract = new InteractEvent();
	public InteractEvent onInteract => m_onInteract;
}
