using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class EventItem
	{
		public string event_id;
		public string event_name;
		public string date;
		public string time;
		public string province;
		public string city;
		public string price; 
		public string event_category;
		public string event_description;
		public string locate_map;
		public string ticket_link;
		public string event_avatar;

		public static EventItem GetEventItem(string jsonString)
		{
			return JsonUtility.FromJson<EventItem>(jsonString);
		}
	}
}

