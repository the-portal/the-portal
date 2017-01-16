using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Requestor : MonoBehaviour
{
	public static WWW download; 
	public static string output; 

	public static IEnumerator SendRequest(IDictionary <string,string> data,string table,string page,string sceneToLoad)
	{
		output = null;
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table",table);

		foreach(KeyValuePair<string,string>kvp in data)
        {
			form.AddField(kvp.Key,kvp.Value);
			Debug.Log(kvp.Key + " " + kvp.Value);
        }

		// Create a download object
		download = new WWW(UniversalURL.url + page, form);

		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
			//Debug.Log(download.text);
			string[] results  = download.text.Split('|');
			if (download.text.Length == 0)
			{
				if(sceneToLoad != null)
					Application.LoadLevel(sceneToLoad);
			}
//			else
//			if(download.text.Equals("login"))
//			{
//				PlayerPrefs.SetString("user",inputFields[0].text);
//				AccountManager.accountType = (inputFields[0].name == "username")?"explorer":"promoter";
//				PlayerPrefs.SetString("accountType",AccountManager.accountType);
//				Application.LoadLevel(sceneToLoad);
//			}

			else
			if(results[0] == "event_id")
			{
				output = results[1];
				Debug.Log(output);
			}
			else
			{
				//Error message on UI put text as param
				Debug.Log(download.text);
			}
		}
	}

}

