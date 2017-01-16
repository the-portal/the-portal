using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Login : MonoBehaviour {

	// Use this for initialization
	public InputField[] inputFields;
	public string page;
	public string table;
	public string sceneToLoad;
	public Text errorMessage;
	void Start()
	{
//		Debug.Log (AssemblyCSharp.PasswordEncryption.Encryption ("fixed"));
	}

	public void Submit()
	{
		string results = Validator.CheckEmptyFields (inputFields);
		if (results.Length < 1) {
				StartCoroutine(LoginRequest());
		} else {
			EnableErrorMessage(results);
		}
	}

	public IEnumerator LoginRequest()
	{
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table",table);
		form.AddField(inputFields[0].name,inputFields[0].text);
		form.AddField(inputFields[1].name,inputFields[1].text);

//		form.AddField(inputFields[1].name,AssemblyCSharp.PasswordEncryption.Encryption(inputFields[0].text));

		// Create a download object
		WWW download = new WWW(UniversalURL.url + page, form);

		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			//print( "Error downloading: " + download.error );
			EnableErrorMessage("No Connection");
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
			{

				if(results[0] == "login")
				{
					//Debug.Log(results[1]);
					//PlayerPrefs.SetString("avatar",results[1]);
					PlayerPrefs.SetString("user",inputFields[0].text);
					AccountManager.user = inputFields[0].text;
					AccountManager.accountType = (inputFields[0].name == "username")?"explorer":"promoter";
					PlayerPrefs.SetString("accountType",AccountManager.accountType);

					AccountManager.fields = new Dictionary<string,string>();
					string[] data = results[1].Split('#');
					string[] keys = data[0].Split(',');
					string[] values = data[1].Split(',');
					for(int i = 0;i<keys.Length;i++)
					{
						AccountManager.fields[keys[i]] = values[i];
//						Debug.Log(keys[i] + " " + values[i]);
					}

					Application.LoadLevel(sceneToLoad);
				}
				else
				{
					//Error message on UI put text as param
					EnableErrorMessage(download.text);
					Debug.Log(download.text);
				}
			}
		}
	}

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}
}
