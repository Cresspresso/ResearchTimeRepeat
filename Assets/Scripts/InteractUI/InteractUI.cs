using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
	public CanvasScaler canvasScaler { get; private set; }

	public PlayerHand playerHand;

	public bool onlyIfInteractable = false;

	public GameObject visuals;
	public Text textElement;
	private bool m_visible;
	public bool visible {
		get => m_visible;
		set
		{
			m_visible = value;
			visuals.SetActive(value);
		}
	}

	private void Awake()
	{
		canvasScaler = GetComponentInParent<CanvasScaler>();

		if (!playerHand)
		{
			playerHand = FindObjectOfType<PlayerHand>();
		}
	}

	private void Start()
	{
		visible = false;
	}

	private void LateUpdate()
	{
		var transform = (RectTransform)this.transform;

		var interactable = onlyIfInteractable
			? playerHand.GetClosestInteractable()
			: playerHand.GetClosestHovered();

		if (interactable && interactable.showHoverInfo)
		{
			if (!visible)
			{
				visible = true;
			}

			textElement.text = playerHand.IsInteractable(interactable)
				? interactable.hoverDescription
				: interactable.hoverNotInteractableDescription;

			var worldPos = interactable.transform.position;
			var screenPos = Camera.main.WorldToViewportPoint(worldPos) * canvasScaler.referenceResolution;
			transform.anchoredPosition = screenPos;
		}
		else
		{
			if (visible)
			{
				visible = false;
			}
		}
	}
}
