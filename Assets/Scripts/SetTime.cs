using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetTime : MonoBehaviour
{
//	int hours;
//	int minutes;
	public Dropdown ddHours;
	public Dropdown ddMinutes;
	public Dropdown ddInterval;
	public GameObject txtTime;
	public GameObject timeComponents;
	// Use this for initialization
	public void EnableTime()
	{
		timeComponents.SetActive(true);
	}
	void Start ()
	{
		List<string> hours = new List<string>();

		for(int i=1;i<=12;i++)
		{
			string temp = (i<10)?"0"+i:i.ToString();
			hours.Add(temp);
		}
		ddHours.AddOptions(hours);

		List<string> minutes = new List<string>();
		for(int i=0;i<=59;i++)
		{
			string temp = (i<10)?"0"+i:i.ToString();
			minutes.Add(temp);
		}
		ddMinutes.AddOptions(minutes);
	}

	public void SetTimeText()
	{
		string time = ddHours.captionText.text + " : " + ddMinutes.captionText.text + " " + ddInterval.captionText.text;
		txtTime.GetComponent<Text>().text = time;
	}
}

