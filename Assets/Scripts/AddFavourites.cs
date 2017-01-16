using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddFavourites : MonoBehaviour {

	public void AddFavourite()
	{
		IDictionary<string,string> favourite = new Dictionary<string,string>();
		favourite.Add("event_id",EventID.eventId);
		favourite.Add("username",PlayerPrefs.GetString("user"));
		StartCoroutine(Requestor.SendRequest(favourite,"favourites","insert.php",null));
	}
}
