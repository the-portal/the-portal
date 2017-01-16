using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageDownloader : MonoBehaviour
{
	void Start()
	{
//		StartCoroutine(DownloadImage("explorers/test.jpg",gameObject));
	}
	public static IEnumerator DownloadImage(string imageSource,GameObject imageHolder) {
		// Start a download of the given URL
		WWW www = new WWW(UniversalURL.url + "img/" + imageSource);

		// Wait for download to complete	
		yield return www;

		// assign texture
//		Debug.Log(imageSource);
		if(www.texture!= null)
		{
			Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
			imageHolder.GetComponent<Image>().sprite = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
		}
	}
}

