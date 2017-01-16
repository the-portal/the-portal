using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DownloadAvatar : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		if(AccountManager.avatar != null &&  PlayerPrefs.GetString("avatar")!="")
		{
			StartCoroutine(ImageDownloader.DownloadImage("explorers/"+PlayerPrefs.GetString("avatar"),gameObject));
			AccountManager.avatar = GetComponent<Image>().sprite;
		}
	}
}

