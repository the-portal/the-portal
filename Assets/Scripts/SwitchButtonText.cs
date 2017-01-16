using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchButtonText : MonoBehaviour
{
	public void SwitchText(string func)
	{
		GetComponent<Text>().text = func;
	}
}

