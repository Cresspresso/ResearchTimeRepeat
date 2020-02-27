using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPanelButton : Interactable
{
	public bool isInteractable { get; set; } = false;

	private void Awake()
	{
		isInteractable = isInteractable;
	}

	public override NotInteractableReason GetNotInteractableReason(InteractEventArgs eventArgs)
	{
		if (!isInteractable) { return new NotInteractableReason("button disabled"); }
		return base.GetNotInteractableReason(eventArgs);
	}

	protected override void OnInteract(InteractEventArgs eventArgs)
	{
		visuals.SetActive(true);
		if (anim) { anim.enabled = true; }
		FindObjectOfType<PlayerController>().isGameControlEnabled = false;
		//base.OnInteract(eventArgs);
	}





	private Animator anim;
	public GameObject visuals;

	private void Start()
	{
		anim = GetComponent<Animator>();
		visuals.SetActive(false);
	}

	private void Update()
	{
		int sceneIndex = 3; // "VictoryVideoScene"
		if (anim)
		{
			if (anim.enabled && anim.IsInTransition(0))
			{
				var info = anim.GetAnimatorTransitionInfo(0);
				if (info.IsUserName("CustomTransition"))
				{
					SceneManager.LoadScene(sceneIndex);
				}
			}
		}
		else if (visuals.activeSelf)
		{
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
