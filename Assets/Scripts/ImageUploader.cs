using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageUploader : MonoBehaviour
{

	// Use this for initialization
	// Use this for initialization
	void Start () {
//		string table = (PlayerPrefs.GetString("accountType")=="explorer")?"explorers":"promoters";
//		string set = PlayerPrefs.GetString("accountType")+"_avatar";
//		Debug.Log(set);

		//StartCoroutine(UploadPNG());
	}

	public static IEnumerator  UploadPNG(Texture2D image,string table,string where,string sceneToLoad,Image avatar,Sprite changeTo) {

			// Encode texture into PNG
			byte[] bytes = image.EncodeToPNG();
			DestroyImmediate(image,true);

			// Create a Web Form
			WWWForm form = new WWWForm();
			form.AddField("table", table);
			form.AddField("where", where);
			form.AddField("set", where);
			form.AddField("image_name", where);
			form.AddBinaryData("img_uploader", bytes, where + ".png", "image/png");

			// Upload to a cgi script
			WWW w = new WWW(UniversalURL.url + "upload_image.php", form);
			yield return w;
			if (!string.IsNullOrEmpty(w.error)) {
				Debug.Log(w.error);
			}
			else {
				if (w.uploadProgress == 1.0) {
					if (sceneToLoad != null) {
						Application.LoadLevel (sceneToLoad);
					}
	//					Debug.Log(w.text);
					if (avatar) {
						avatar.sprite = changeTo;
					}
		 		}
			}
		}
		
		IEnumerator UploadPNGTest() {
		// We should only read the screen after all rendering is complete
		yield return new WaitForEndOfFrame();

		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		var tex = new Texture2D( width, height, TextureFormat.RGB24, false );

		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		// Encode texture into PNG
		byte[] bytes = tex.EncodeToPNG();
		Destroy(tex);

		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("frameCount", Time.frameCount.ToString());
		form.AddField("table", "explorers");
		//form.AddField("set", set);
		//form.AddField("where", where);
		form.AddField("image_name", "screenShot.png");
		form.AddBinaryData("img_uploader", bytes, "screenShot.png", "image/png");

		// Upload to a cgi script
		WWW w = new WWW(UniversalURL.url + "upload_image.php", form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error);
		}
		else {
			print("Finished Uploading Screenshot");
		}
	}
}
