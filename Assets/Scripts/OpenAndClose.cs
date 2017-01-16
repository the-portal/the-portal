using UnityEngine;
using System.Collections;

public class OpenAndClose : MonoBehaviour {
	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
		
	public void CloseParent()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
