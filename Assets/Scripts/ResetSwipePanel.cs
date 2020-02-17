using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSwipePanel : MonoBehaviour
{
	public GroundhogDay groundhogDay;

	private void Awake()
	{
		Debug.Assert(groundhogDay, "groundhogDay is null", this);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "KeyCard")
		{
			groundhogDay.RestartTimeLoop();
		}
	}
}
