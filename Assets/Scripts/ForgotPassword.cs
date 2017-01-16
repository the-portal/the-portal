using UnityEngine;
using System.Collections;

public class ForgotPassword : MonoBehaviour
{
	public static string reset_table;
	public string table;
	// Use this for initialization
	void Start ()
	{
		reset_table = table;
	}
	
	// Update is called once per frame
	public void Submit ()
	{
		
		Application.LoadLevel("Password Recovery");
	}
}

