using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using AssemblyCSharp;

public class Register : MonoBehaviour {
	public string table;
	public string sceneToLoad;
	public Text errorMessage;
	Form formFields;

	public void Submit ()
	{
		formFields = new Form();
		string results = formFields.CheckEmptyFields();
		if (results.Length < 1) {
			results = formFields.PasswordMatch();
			if (results.Length < 1) {
				formFields.fields.Remove("confirm_password");
//				formFields.fields["password"] = PasswordEncryption.Encryption(formFields.fields["password"]);

				StartCoroutine(Request());
			} 
			else {
				EnableErrorMessage(results);
			}
		} else {
			EnableErrorMessage(results);
		}
	}

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}

	public IEnumerator Request()
	{
		WWW download;
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table", table);

		foreach(KeyValuePair<string,string>kvp in formFields.fields)
		{
			form.AddField(kvp.Key,kvp.Value);
		}

		// Create a download object
		download = new WWW(UniversalURL.url +"insert.php", form);

		// Wait until the download is done
		yield return download;

		if (!string.IsNullOrEmpty(download.error))
		{
			//			Debug.Log("Error downloading: " + download.error);
			EnableErrorMessage("No Connection");
		}
		else
		{
			if (download.text.Length == 0)
			{
				Application.LoadLevel(sceneToLoad);
			}
			else
			{
				//Error message on UI put text as param
				Debug.Log(download.text);
				EnableErrorMessage(download.text);
			}
		}
	}
}
