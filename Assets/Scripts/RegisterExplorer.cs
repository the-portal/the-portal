using UnityEngine;
using UnityEngine.UI;
using System;

public class RegisterExplorer : MonoBehaviour
{
	[SerializeField]
	private InputField[] fields;
	[SerializeField]
	private string url;
	[SerializeField]
	private string table;
	[SerializeField]
	string sceneToLoad;
	Text errorMessage;

	public void Submit ()
	{
		try {
			string results = Validator.CheckEmptyFields (fields);
			if (results.Length < 1) {
				results = Validator.FieldsValidtionsExplorer (fields [1], fields [2], fields [3], fields [4]);
				if (results.Length < 1) {
					StartCoroutine (RequestSender.SendRequest (fields, table, url, sceneToLoad));
				} else {
					EnableErrorMessage(results);
				}
			} else {
				EnableErrorMessage(results);
			}
		} catch (Exception e) {
			Debug.LogException(e, this);
		}
	}

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}
}
