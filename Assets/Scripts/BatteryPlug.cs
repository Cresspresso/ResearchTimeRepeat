using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPlug : MonoBehaviour
{
	public BatteryLightCave blc;
	public WorldItem battery = null;

	void OnTriggerEnter(Collider other)
	{
		battery = other.GetComponentInParent<WorldItem>();
		if (battery)
		{
			blc.Power(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponentInParent<WorldItem>() == battery)
		{
			battery = null;
			blc.Power(false);
		}
	}
}
