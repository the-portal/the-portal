using UnityEngine;
using System.Collections;

public class Sort : MonoBehaviour
{
	public static string orderBy;
	void Start()
	{
		orderBy = name;
	}
	public static string OrderBy()
	{
		GlobalData.eventsData = null;
		return orderBy;
		//Invoke("MyEvents.GetMenuItems",0.5);
	}
}

