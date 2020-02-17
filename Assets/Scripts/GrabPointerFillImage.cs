using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabPointerFillImage : MonoBehaviour
{
	public Image fillImage;

	private GrabPointer gp;

	private void UpdateGrab(GrabPointer sender, float elapsedTime)
	{
		fillImage.fillAmount = elapsedTime / sender.goalTime;
	}

	void OnEnable()
	{
		if (!gp)
		{
			gp = GetComponent<GrabPointer>();
			gp.onUpdateGrab.AddListener(UpdateGrab);

			UpdateGrab(gp, gp.elapsedTime);
		}
	}

	void OnDisable()
	{
		if (gp)
		{
			try
			{
				gp.onUpdateGrab.RemoveListener(UpdateGrab);
			}
			finally
			{
				gp = null;
			}
		}
	}
}
