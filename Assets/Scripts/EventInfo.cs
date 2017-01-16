using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventInfo : MonoBehaviour
{
	public GameObject eventController;
	public Text eventInfo;
	public Text eventName;
	
	public void DisplayEventInfo ()
	{
		string[] fields = new string[]{"Date","Time","Province","City","Price","Event Category","Event Description","Map Location","Ticket Purchase Link"};
		eventController.SetActive(true);
		string[] eventData = GlobalData.eventsData[name];
		eventName.text = eventData[1]; 	
		eventInfo.text = "";
		Debug.Log(fields.Length + " " + eventData.Length);
		for(int x =2;x<eventData.Length;x++)
		{
			if(x<fields.Length+2)
			{
				eventInfo.text += fields[x-2] +" : "+eventData[x] +"\n";
			}
		}
	}
}

