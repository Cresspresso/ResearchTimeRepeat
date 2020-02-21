using System.Collections;
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
		SceneManager.LoadScene(1);
	}
}
