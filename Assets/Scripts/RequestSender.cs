using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RequestSender : MonoBehaviour
{
	public static WWW download; 
	public static string output; 
	public static IEnumerator SendRequest(InputField[] inputFields,string table,string page,string sceneToLoad)
	{
		// Create a form object for sending high score data to the server
		output = null;
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table",table);

		foreach(InputField inputField in inputFields)
		{
			form.AddField(inputField.name,inputField.text);
			//Debug.Log(inputField.name + "  "  + inputField.text);
		}

		// Create a download object
		download = new WWW(UniversalURL.url + page, form);

		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
            // show the highscores
            //Debug.Log(download.text);
			string[] results  = download.text.Split('|');
            if (download.text.Length == 0)
            {
				if(sceneToLoad != null)
                Application.LoadLevel(sceneToLoad);
            }
            else
			if(results[0] == "event_id")
			{
				output = results[1];
				Debug.Log(output);
			}
			else
			if(results[0] == "login")
            {
				PlayerPrefs.SetString("avatar",results[1]);
                PlayerPrefs.SetString("user",inputFields[0].text);
				AccountManager.user = inputFields[0].text;
				AccountManager.accountType = (inputFields[0].name == "username")?"explorer":"promoter";
				PlayerPrefs.SetString("accountType",AccountManager.accountType);
                Application.LoadLevel(sceneToLoad);
            }
            else
            {
                //Error message on UI put text as param
                Debug.Log(download.text);
            }
		}
	}

}

