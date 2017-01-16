using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class AccountManager : MonoBehaviour {
	
	public static string user; 
	public static string accountType; 
	public static Sprite avatar; 
	public static IDictionary<string,string> fields;

	public void Logout()
	{
		PlayerPrefs.DeleteAll();
		fields.Clear();
		Application.LoadLevel("SignUp Login");
	}
}
