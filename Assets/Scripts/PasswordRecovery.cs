using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PasswordRecovery : MonoBehaviour {

	// Use this for initialization
	public InputField code;
	public InputField email;
	public InputField password;
	public InputField confirmPassword;
	public string page;
	public string table;
	public Text errorMessage;
	public Text title;

	public void Start()
	{
		table = ForgotPassword.reset_table;
	}
	public void Submit()
	{
		if(password.IsActive ()) {
			string results = Validator.PasswordMatch (password,confirmPassword);
			if (results==null) {
				page = "reset_password.php";
				StartCoroutine(Request("password",password.text));

			} else {
				EnableErrorMessage (results);
				return;
			}
		}
		if(code.IsActive ()) {
			string results = Validator.Empty (code);
			if (results==null) {
				page = "reset_code.php";
				StartCoroutine(Request("code",code.text));

			} else {
				EnableErrorMessage (results);
				return;
			}
		}
		if (email.IsActive ()) {
			string results = Validator.EmailValid (email);
			if (results ==null) {
				page = "forgot_password.php";
				StartCoroutine(Request());
			} else {
				EnableErrorMessage (results);
				return;
			}
		}
	}

	public IEnumerator Request(string field="null",string input ="null")
	{
		// Create a form object for sending high score data to the server
//		Debug.Log ("page :" + page + "field :" + field + " table :" + ForgotPassword.reset_table + " input :" + input.text);

		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table",table);
		form.AddField("email",email.text);
		form.AddField(field,input);
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
			Debug.Log(download.text);
			if (download.text.Length == 0)
			{
				string sceneToLoad = (table=="explorers")?"Login Explorer":"Login Promoter";
				Application.LoadLevel(sceneToLoad);
			}
			else
			{
				if(download.text == "sent")
				{
					title.text = "Code sent \nto your email";
					email.gameObject.SetActive (false);
					code.gameObject.SetActive (true);
				}
				else
				if(download.text == "code")
				{
					title.text = "Reset Password";
					password.gameObject.SetActive (true);
					confirmPassword.gameObject.SetActive (true);
					code.gameObject.SetActive (false);
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
