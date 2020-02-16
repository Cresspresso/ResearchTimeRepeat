using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundhogDay : MonoBehaviour
{
	public float duration = 20.0f;
	public float startDelay = 1.0f;
	public float timeLoopRestartDelay = 1.0f;

	private float m_elapsedTime;

	public float elapsedTime => Mathf.Clamp(m_elapsedTime, 0.0f, duration);
	public float remainingTime => Mathf.Clamp(duration - m_elapsedTime, 0.0f, duration);
	public bool hasEnded => m_elapsedTime >= duration;

	private void Start()
	{
		m_elapsedTime = -startDelay;
	}

	private void LateUpdate()
	{
		m_elapsedTime += Time.deltaTime;
		if (m_elapsedTime >= duration + timeLoopRestartDelay)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
