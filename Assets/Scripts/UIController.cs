using UnityEngine;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
	[System.Serializable]
	public class MenuUI {
		public RectTransform title, instructions, highScore, buttonExit, panelButton, helpers;
	}
	public MenuUI menuUI;

	public Menu menu;

	public float originWidth, originHeight;

	Vector2 storedRes;

	Dictionary<string, Vector2> originPositions = new Dictionary<string, Vector2>();
	Dictionary<string, Vector2> originSizes = new Dictionary<string, Vector2>();

	void Start ()
	{
		SetOrigin();
		Adjust(Screen.width, Screen.height);
		menu.instructionsObj.SetActive(false);
	}

	void Update ()
	{
		if (Screen.width != storedRes.x || Screen.height != storedRes.y)
			Adjust(Screen.width, Screen.height);
	}

	void SetOrigin ()
	{
		RectTransform[] targets = FindObjectsOfType<RectTransform>();
		foreach (RectTransform t in targets)
		{
			originPositions.Add(t.name, t.anchoredPosition);
			originSizes.Add(t.name, t.sizeDelta);
		}
	}

	Vector2 GetOriginPos (string name)
	{
		Vector2 value = Vector2.zero;
		if (originPositions.TryGetValue(name, out value))
			return value;
		else
			return Vector2.zero;
	}

	Vector2 GetOriginSize (string name)
	{
		Vector2 value = Vector2.zero;
		if (originSizes.TryGetValue(name, out value))
			return value;
		else
			return Vector2.zero;
	}

	void Adjust (float width, float height)
	{
		// ? List all UI items
		RectTransform[] targets = FindObjectsOfType<RectTransform>();
		foreach (RectTransform t in targets)
		{
			Vector2 originPos = GetOriginPos(t.name);
			if (originPos == Vector2.zero)
				originPos = t.anchoredPosition;
			t.anchoredPosition = new Vector2 ((originPos.x / originWidth) * width, (originPos.y / originHeight) * height);

			Vector2 originSize = GetOriginSize(t.name);
			if (originSize == Vector2.zero)
				originSize = t.sizeDelta;
			t.sizeDelta = new Vector2 ((originSize.x / originWidth) * width, (originSize.y / originHeight) * height);
		}
		storedRes = new Vector2 (width, height);
	}
}