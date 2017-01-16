using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp

{
	public class EventItemForm : Form
	{
		public string eventName;
		public string date;
		public string time;
//		public string time;

		public EventItemForm ():base()
		{
			
		}

		public void SetEventItem (EventProperties eventProperties)
		{
			eventProperties.eventName.text = fields["event_name"];
			eventProperties.date.text = fields["date"];
			eventProperties.venue.text = fields["province"] + "," +fields["city"];
//			eventProperties.avatar = fields["province"] + "," +fields["city"];
		}
	}
}