using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindUI : MonoBehaviour
{
	public Animator anim;

	private void Update()
	{
		if (GroundhogDay.instance.isRewinding)
		{
			anim.SetTrigger("Rewind");
			this.enabled = false;
		}
	}
}
