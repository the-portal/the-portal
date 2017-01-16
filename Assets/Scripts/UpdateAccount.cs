using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using AssemblyCSharp;

public class UpdateAccount  : MonoBehaviour
{
	public string table;
	public Text errorMessage;
	Form formFields;
	public void Submit ()
	{
		formFields = new Form();
		//string results = formFields.CheckEmptyFields ();
//		if (results.Length < 1) {
//			results = Validator.FieldsValidtionsExplorer (fields [1], fields [2], fields [3], fields [4]);
			string results = formFields.PasswordMatch();
			if (results.Length < 1) {
				StartCoroutine (Request ());
			} else {
				EnableErrorMessage(results);
			}
//		} else {
//			EnableErrorMessage(results);
//		}
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
			if(kvp.Value.Length!=0)
			{
				form.AddField(kvp.Key,kvp.Value);
			}
		}

		// Create a download object
		download = new WWW(UniversalURL.url +" update.php", form);

		// Wait until the download is done
		yield return download;

		if (!string.IsNullOrEmpty(download.error))
		{
			Debug.Log("Error downloading: " + download.error);
	//		EnableErrorMessage("No Connection");
		}
		else
		{
			string[] results  = download.text.Split('|');
			if (download.text.Length == 0)
			{
				foreach( string key in AccountManager.fields.Keys)
				{
					AccountManager.fields[key] = formFields.fields[key];
				}
				transform.parent.gameObject.SetActive(false);
			}
			else
			{
				//Error message on UI put text as param
				EnableErrorMessage(download.text);
			}
		}
	}

	public void Setform()
	{
		formFields = new Form();
		AccountManager.fields.Remove("password");
//		string[] fields = new string[AccountManager.fields.Values.Count];
//		AccountManager.fields.Values.CopyTo(fields,0);
		formFields.SetForm(AccountManager.fields);
	}
}