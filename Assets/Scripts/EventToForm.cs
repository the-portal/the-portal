using UnityEngine;
using System.Collections;

public class EventToForm : MonoBehaviour
{
	public GameObject eventForm;
	public static GameObject eventItem;
	// Use this for initialization

	public void LoadEventToForm()
	{
		eventItem = transform.parent.gameObject;
		eventForm.SetActive (true);
	}

	public void ResetEventItem()
	{
		eventItem = null;
	}
}