using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GenericRequestor : MonoBehaviour
{
	public static string output; 

	public static IEnumerator CreateEvent(WWWForm form,string page)
	{
		output = null;
		// Create a download object
		WWW download = new WWW(UniversalURL.url + page, form);

		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
			//Debug.Log(download.text);
			string[] results  = download.text.Split('|');

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


