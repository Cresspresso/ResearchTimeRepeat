using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EscapeZone : MonoBehaviour
{
	public bool isEscaping { get; private set; } = false;
	private bool isPlayingVideo = false;

	private VideoPlayer videoPlayer;

	private void OnTriggerEnter(Collider other)
	{
		if (isEscaping) { return; }

		if (other.GetComponentInParent<PlayerController>())
		{
			isEscaping = true;

			var gd = FindObjectOfType<GroundhogDay>();
			if (gd)
			{
				gd.enabled = false;
			}

			// play the video
			videoPlayer = Camera.main.GetComponent<VideoPlayer>();
			videoPlayer.started += (sender) => isPlayingVideo = true;
			videoPlayer.Play();
		}
	}

	private void Update()
	{
		if (this.isPlayingVideo && !videoPlayer.isPlaying)
		{
			SceneManager.LoadScene(0);
		}
	}
}
