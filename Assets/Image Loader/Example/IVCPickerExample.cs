using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ImageVideoContactPicker;

public class IVCPickerExample : MonoBehaviour {

	string log = "";
	public static string imagePath;
	public static Texture2D texture;
	public Image imageSpr;
	//public GameObject thidHide;
	public GameObject afterSelect;
	private Image _imageHolder;
	public static bool changeAvatar = false;

	void OnEnable()
	{
		PickerEventListener.onImageSelect += OnImageSelect;
		PickerEventListener.onImageLoad += OnImageLoad;
		//PickerEventListener.onVideoSelect += OnVideoSelect;
		//PickerEventListener.onContactSelect += OnContactSelect;
		PickerEventListener.onError += OnError;
		PickerEventListener.onCancel += OnCancel;
	}
	
	void OnDisable()
	{
		PickerEventListener.onImageSelect -= OnImageSelect;
		PickerEventListener.onImageLoad -= OnImageLoad;
		//PickerEventListener.onVideoSelect -= OnVideoSelect;
	//	PickerEventListener.onContactSelect -= OnContactSelect;
		PickerEventListener.onError -= OnError;
		PickerEventListener.onCancel -= OnCancel;
	}

	
	void OnImageSelect(string imgPath)
	{
		Debug.Log ("Image Location : "+imgPath);
		log += "\nImage Path : " + imgPath;
		imagePath = imgPath;
	}


	void OnImageLoad(string imgPath, Texture2D tex)
	{
		Debug.Log ("Image Location : "+imgPath);
		imagePath = imgPath;
		texture = tex;
		imageSpr.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.7f,0.7F));
	}

	void OnError(string errorMsg)
	{
		Debug.Log ("Error : "+errorMsg);
		log += "\nError :" +errorMsg;
		log += "\n emotional:" +errorMsg;
		//thidHide.SetActive (true);
		afterSelect.SetActive (false);
	}

	void OnCancel()
	{
		Debug.Log ("Cancel by user");
		log += "\nCancel by user";
		//thidHide.SetActive (true);
		afterSelect.SetActive (true);
	}

	Vector2 sp1 = Vector2.zero;
	Vector2 sp2 = Vector2.zero;

	public void imageMe()
	{
		//yield WaitForSeconds("1");
		//thidHide.SetActive (false);
		afterSelect.SetActive (true);
		#if UNITY_ANDROID
	 	AndroidPicker.BrowseImage(true);
		#elif UNITY_IPHONE
		IOSPicker.BrowseImage(false); // pick
		#endif
	}

}
