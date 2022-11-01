using UnityEngine;
using System.Collections;

[System.Serializable]
public class GUIController
{
    [SerializeField]
    private string m_prefabPath;

	[SerializeField]
	protected UICanvas m_canvas;

	protected UICanvas m_canvasInstance;

	public virtual bool Show() 
	{
		if (m_canvasInstance != null) {
			return false;
		}

        if (m_canvas == null)
        {
            m_canvas = Resources.Load<UICanvas>(m_prefabPath);
        }

		m_canvasInstance = GameObject.Instantiate (m_canvas);
		GameObject.DontDestroyOnLoad (m_canvasInstance);

		return true;
	}

	public virtual void Hide()
	{
		if (m_canvasInstance == null) {
			return;
		}

		GameObject.Destroy (m_canvasInstance.gameObject);
        m_canvas = null;
	}
}

[System.Serializable]
public class MessageBox: GUIController {
	public System.Action m_OnOk;

	public override bool Show ()
	{
		base.Show ();
		MessageboxCanvas canvas = m_canvasInstance as MessageboxCanvas;
		canvas.m_OnOkClick = OnOkClick;
		return true;
	}

	public void Show(string title, string message) {
		Show ();
		MessageboxCanvas canvas = m_canvasInstance as MessageboxCanvas;
		canvas.SetTitle (title);
		canvas.SetMessage (message);
	}

	private void OnOkClick(UICanvas sender) {
		if (m_OnOk != null) {
			m_OnOk.Invoke ();
		}
		this.Hide ();
	}
}
