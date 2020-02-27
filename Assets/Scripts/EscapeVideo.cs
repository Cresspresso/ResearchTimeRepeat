using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EscapeVideo : MonoBehaviour
{
	private VideoPlayer videoPlayer;
	private bool doing = false;

	private void Awake()
	{
		videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.started += (sender) => doing = true;
	}

	private void Update()
	{
		if (doing && !videoPlayer.isPlaying)
		{
			SceneManager.LoadScene(0);
		}
	}
}
