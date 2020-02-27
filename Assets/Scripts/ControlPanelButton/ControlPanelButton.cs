﻿using System.Collections;
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
		var gd = FindObjectOfType<GroundhogDay>();
		if (gd.isGameEnding == false)
		{
			gd.isGameEnding = true;
			visuals.SetActive(true);
			if (anim) { anim.enabled = true; }
			FindObjectOfType<PlayerController>().isGameControlEnabled = false;
			//base.OnInteract(eventArgs);
		}
	}





	public Animator anim;
	public GameObject visuals;

	public bool isAborting => anim ? anim.enabled : visuals.activeSelf;

	private void Start()
	{
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
