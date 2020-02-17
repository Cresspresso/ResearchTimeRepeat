using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTicker : MonoBehaviour
{
	public GroundhogDay groundhogDay;
	public Text clockText;

	private void Update()
	{
		 clockText.text = string.Format("{0:00.00}", groundhogDay.remainingTime);
	}
}
