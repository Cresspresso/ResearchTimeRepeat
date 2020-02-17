using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartUnlock : MonoBehaviour
{
	public playermovement playerMovement;
	public GroundhogDay groundhogDay;

	private void Start()
	{
		playerMovement.isHumanControlEnabled = false;
	}

	private void Update()
	{
		if (groundhogDay.elapsedTime > 0.0f)
		{
			playerMovement.isHumanControlEnabled = true;
			Destroy(this);
		}
	}
}
