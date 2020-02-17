using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GrabPointer : MonoBehaviour
{
	public float goalTime = 0.5f;
	public float elapsedTime { get; private set; } = 0.0f;
	public bool isFinishedGrab { get; private set; } = false;
	public bool isGrabbing => elapsedTime > 0.0f;

	[System.Serializable]
	public class GrabEvent : UnityEvent<GrabPointer, float> { }

	[HideInInspector]
	public GrabEvent onBeginGrab = new GrabEvent();
	[HideInInspector]
	public GrabEvent onEndGrab = new GrabEvent();
	[HideInInspector]
	public GrabEvent onUpdateGrab = new GrabEvent();
	[HideInInspector]
	public GrabEvent onCancelGrab = new GrabEvent();

	private void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if (elapsedTime <= 0.0f)
			{
				Utils.TryExceptLog(this, () => onBeginGrab.Invoke(this, 0.0f));
			}
			
			if (!isFinishedGrab)
			{
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= goalTime)
				{
					isFinishedGrab = true;
					Utils.TryExceptLog(this, () => onUpdateGrab.Invoke(this, goalTime));
					Utils.TryExceptLog(this, () => onEndGrab.Invoke(this, goalTime));
					elapsedTime = 0.0f;
					Utils.TryExceptLog(this, () => onUpdateGrab.Invoke(this, 0.0f));
				}
				else
				{
					Utils.TryExceptLog(this, () => onUpdateGrab.Invoke(this, elapsedTime));
				}
			}
		}
		else
		{
			isFinishedGrab = false;
			if (elapsedTime > 0.0f)
			{
				Utils.TryExceptLog(this, () => onUpdateGrab.Invoke(this, elapsedTime));
				Utils.TryExceptLog(this, () => onCancelGrab.Invoke(this, elapsedTime));
				elapsedTime = 0.0f;
				Utils.TryExceptLog(this, () => onUpdateGrab.Invoke(this, 0.0f));
			}
		}
	}
}
