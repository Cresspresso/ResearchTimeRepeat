using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundhogDay : MonoBehaviour
{
	// Inspector Fields

	public float duration = 20.0f;
	public float startDelay = 1.0f;
	public float timeLoopRestartDelay = 1.0f;


	// Non Serialized Fields

	private float m_elapsedTime;
	public bool isLoadingScene { get; private set; }


	// Properties

	public float elapsedTime => Mathf.Clamp(m_elapsedTime, 0.0f, duration);
	public float remainingTime => Mathf.Clamp(duration - m_elapsedTime, 0.0f, duration);
	public bool hasEnded => m_elapsedTime >= duration;


	// Unity Methods

	private void Start()
	{
		m_elapsedTime = -startDelay;
	}

	public void RestartTimeLoop()
	{
		isLoadingScene = true;
		m_elapsedTime = duration + timeLoopRestartDelay;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Debug.Log("Groundhog Day is Restarting.");
	}

	private void LateUpdate()
	{
		if (!isLoadingScene && m_elapsedTime >= duration + timeLoopRestartDelay)
		{
			RestartTimeLoop();
		}
		else
		{
			m_elapsedTime += Time.deltaTime;
		}
	}
}
