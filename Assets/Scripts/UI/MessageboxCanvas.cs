using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageboxCanvas : UICanvas {

	public OnUIClick m_OnOkClick;

	[SerializeField] private Text m_TitleText;
	[SerializeField] private Text m_MessageText;

	public void OnOKClick() {
		if (m_OnOkClick != null)
			m_OnOkClick.Invoke (this);
	}

	public void SetTitle(string title) {
		if (m_TitleText != null)
			m_TitleText.text = title;
		else
			Debug.LogWarning ("Title Text is lost reference.");
	}

	public void SetMessage(string message) {
		if (m_MessageText != null) {
			m_MessageText.text = message;
		} else {
			Debug.LogWarning ("Message Text is lost reference.");
		}
	}

}
