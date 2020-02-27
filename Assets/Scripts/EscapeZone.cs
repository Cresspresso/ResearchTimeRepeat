using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeZone : MonoBehaviour
{
	private Animator anim;
	public GameObject visuals;

	private void Start()
	{
		anim = GetComponent<Animator>();
		visuals.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<PlayerController>())
		{
			visuals.SetActive(true);
			anim.enabled = true;
			FindObjectOfType<PlayerController>().isGameControlEnabled = false;
		}
	}

	private void Update()
	{
		if (anim.enabled && anim.IsInTransition(0))
		{
			var info = anim.GetAnimatorTransitionInfo(0);
			if (info.IsUserName("CustomTransition"))
			{
				SceneManager.LoadScene(2);// "EscapeVideoScene"
			}
		}
	}
}
