﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPlayButton : MonoBehaviour
{
	[SerializeField]
	private Button m_button;
	public Button button {
		get
		{
			if (!m_button)
			{
				m_button = GetComponent<Button>();
			}
			return m_button;
		}
	}

	[SerializeField]
	private Animator m_anim;
	public Animator anim => m_anim;

	public float delay = 5.0f;
	public bool isPlaying = false;

	private void Awake()
	{
		button.onClick.AddListener(OnClicked);
	}

	private void OnDestroy()
	{
		button.onClick.RemoveListener(OnClicked);
	}

	private void OnClicked()
	{
		isPlaying = true;
		PlayAnimations();
	}

	private void Update()
	{
		if (isPlaying)
		{
			delay -= Time.deltaTime;
			if (delay <= 0.0f)
			{
				SceneManager.LoadScene(1);
			}
		}
	}

	private void PlayAnimations()
	{
		anim.enabled = true;
	}
}
