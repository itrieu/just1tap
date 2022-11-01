using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	private static GUIManager s_guiManager;

	[SerializeField]
	public MessageBox m_messageBox;

	[SerializeField]
	private HomeController m_homeController;

	void Awake() 
	{
		if (s_guiManager != null) 
		{
			DestroyObject (gameObject);
			return;
		}

		s_guiManager = this;
		DontDestroyOnLoad (gameObject);
	}

	public static GUIManager getInstance() 
	{
		if (s_guiManager == null) 
		{
			GameObject obj = new GameObject ("GUIManager");
			s_guiManager = obj.AddComponent<GUIManager> ();
		}

		return s_guiManager;
	}

	public MessageBox getMessageBox() {
		return m_messageBox;
	}

	public HomeController getHomeController()
	{
		return m_homeController;
	}
}
