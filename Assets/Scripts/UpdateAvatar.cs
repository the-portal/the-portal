using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UpdateAvatar : MonoBehaviour
{
	public string table;  
	// Use this for initialization
	public Sprite intialImage;
	public Image avatar;

	void Start ()
	{
//		avatar
		DownloadImage();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void UploadImage()
	{
		if (intialImage != GetComponent<Image>().sprite) {
			string where = (table=="events")?gameObject.transform.parent.name:PlayerPrefs.GetString ("user");

			StartCoroutine(ImageUploader.UploadPNG(SpriteToTexture2D.Convert(GetComponent<Image>().sprite),table,where,null,avatar,GetComponent<Image>().sprite));
			Debug.Log ("not same");
		} else 
		{
			Debug.Log ("same");
		}
	}

	IEnumerator DownloadImage() {
		// Start a download of the given URL
		WWW www = new WWW(UniversalURL.url + "img/explorers/"+PlayerPrefs.GetString("avatar"));

		// Wait for download to complete	
		yield return www;

		// assign texture
		//		Debug.Log(imageSource);
		if(www.texture!= null)
		{
			Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
			GetComponent<Image>().sprite = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
			intialImage = GetComponent<Image>().sprite;
		}
	}
}

