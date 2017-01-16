using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class EventProperties//Change to EventItem
	{
		public Text eventName;
		public Text date;
		public Text venue;
//		public Image avatar;

		public EventProperties (Button eventItem)
		{
			eventName = eventItem.transform.GetChild(0).GetComponent<Text>();
			date = eventItem.transform.GetChild(1).GetComponent<Text>();
			venue = eventItem.transform.GetChild(2).GetComponent<Text>(); 
//			avatar = eventItem.transform.GetChild(3).GetComponent<Image>(); 
		}
	}
}

