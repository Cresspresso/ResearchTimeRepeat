using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartUnlock : MonoBehaviour
{
	public PlayerController player;
	public GroundhogDay groundhogDay;

	private void Awake()
	{
		if (!groundhogDay)
		{
			groundhogDay = FindObjectOfType<GroundhogDay>();
		}
		Debug.Assert(groundhogDay, "groundhogDay is null", this);

		if (!player)
		{
			player = FindObjectOfType<PlayerController>();
		}
		Debug.Assert(player, "player is null", this);
	}

	private void Start()
	{
		player.isGameControlEnabled = false;
	}

	private void Update()
	{
		if (groundhogDay.elapsedTime > 0.0f)
		{
			player.isGameControlEnabled = true;
			Destroy(this);
		}
	}
}
