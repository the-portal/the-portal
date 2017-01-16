using UnityEngine;
using System.Collections;

public class EventID : MonoBehaviour {

	// Use this for initialization
	public static string eventId;
	public void SetEventId() {
		eventId = name;
//		Debug.Log(eventId);
	}
}
