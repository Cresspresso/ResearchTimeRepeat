using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
	public Animator[] anims = new Animator[1];

	public void OpenDoors()
	{
		if (anims != null)
		{
			foreach (var anim in anims)
			{
				if (anim)
				{
					anim.SetBool("doorOpen", true);
				}
				else
				{
					Debug.LogError("animator is null", this);
				}
			}
		}
	}
}
