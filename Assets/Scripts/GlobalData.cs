using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalData : MonoBehaviour
{
	public static SortedDictionary<string, string[]> eventsData =  
			new SortedDictionary<string, string[]>();
	public static SortedDictionary<string, GameObject> eventItems=  
		new SortedDictionary<string, GameObject>();

	public static void ResetGlobalData()
	{
		eventsData = new SortedDictionary<string, string[]>();
		eventItems = new SortedDictionary<string, GameObject>();
	}
}



