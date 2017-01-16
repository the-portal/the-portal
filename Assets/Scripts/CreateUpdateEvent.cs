using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CreateUpdateEvent:MonoBehaviour
{
	private GameObject eventItem;
	public GameObject eventForm;
    private InputField[] fields;
	private string[] eventData;
	public Text errorMessage;
	string func;
	string eventId;
	public Text btnText;
	EventItemForm formFields;
	public GameObject prefab;
	EventProperties eventProperties;
	public Text formTitle;
	public Sprite intialImage;
	public Image avatar;
	public string table;  


	void Start()
	{
		//		avatar
//		intialImage = avatar.sprite;
	}

	public void InitialAvatar ()
	{
		//		avatar
		intialImage = avatar.sprite;
	}

	public void InitialiseFields(){
		formFields = new EventItemForm();
		eventItem = EventToForm.eventItem;
		eventData = GlobalData.eventsData [eventItem.name];
		formFields.SetForm(eventData);
	}

	public void ResetForm()
	{
		formFields = new EventItemForm();
		formFields.ResetForm();
	}

    public void InsertUpdate()
    {
		formFields = new EventItemForm();
		if(btnText.text!="Create")
		{
			InitialiseFields ();
			func = "update";
			formTitle.text = "New Event";
		}
		else{
			func = "get_event_id";
			formTitle.text = "Update Event";
		}

		string results = formFields.CheckEmptyFields();

		if (results.Length < 1) {
				StartCoroutine (SendRequest());
		} else {
			EnableErrorMessage(results);
		}
    }

	public IEnumerator SendRequest()
    {
        WWW download;
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();
        // Assuming the perl script manages high scores for different games
		form.AddField("table", table);
		if(eventItem)
		{
			form.AddField("event_id", eventItem.name);
		}
		else
		{
			form.AddField("account",PlayerPrefs.GetString("user") );
		}
		
		foreach(KeyValuePair<string,string>kvp in formFields.fields)
		{
			form.AddField(kvp.Key,kvp.Value);
		}

        // Create a download object
		download = new WWW(UniversalURL.url + func + ".php", form);

        // Wait until the download is done
        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
//			Debug.Log("Error downloading: " + download.error);
			EnableErrorMessage("No Connection");
        }
        else
        {
			string[] results  = download.text.Split('|');
            if (download.text.Length == 0)
            {
				int i = 1;//start counting from 1 to skip event_id
				foreach(KeyValuePair<string,string>kvp in formFields.fields)
				{
					eventData [i++] = kvp.Value;
				}
				GlobalData.eventsData [eventItem.name] = eventData;
				formFields.SetEventItem(new EventProperties(eventItem.GetComponent<Button>()));
				eventForm.SetActive (false);
				eventItem = null;
				UploadImage(eventItem.transform.GetChild(3).GetComponent<Image>(),eventItem.name);
//				eventItem.transform.GetChild(3).GetComponent<Image>().sprite = intialImage;
//				Debug.Log(eventItem.name);
//				Debug.Log (eventItem.transform.GetChild (3).GetComponent<Image> ().sprite.name); 
				EnableErrorMessage(eventData[1] + " event \n updated successfully" );
            }
			else
			if(results[0] == "event_id")
			{
//					Debug.Log(results[1]);
				if(GlobalData.eventsData.Count<1)
				{
					RectTransform myRect = GetComponent<RectTransform>();
					GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
					grid.cellSize = new Vector2(myRect.rect.width,myRect.rect.height / 2);
				}
				eventData = new string[formFields.fields.Count + 1];
				eventData[0] = results[1];
				int i = 1;//start counting from 1 to skip event_id
				foreach(KeyValuePair<string,string>kvp in formFields.fields)
				{
					eventData [i++] = kvp.Value;
				}
				prefab.SetActive(true);
				GlobalData.eventsData.Add(results[1],eventData);
				Button button = Instantiate(prefab.GetComponent<Button>());
				prefab.SetActive(false);
				formFields.SetEventItem(new EventProperties(button));
				button.transform.SetParent(transform,false);
				button.tag = "menuItem";
				button.name = eventData[0];
				UploadImage(button.transform.GetChild(3).GetComponent<Image>(),button.name);
//				button.transform.GetChild(3).GetComponent<Image>().sprite = intialImage;
//					Debug.Log(button.transform.GetChild(3).GetComponent<Image>().sprite.name);
				eventForm.SetActive (false);
				EnableErrorMessage(eventData[1] + " event \n created successfully" );
			}
            else
            {
                //Error message on UI put text as param
					EnableErrorMessage(download.text);
					Debug.Log(download.text);
            }
        }
    }

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}

	public void UploadImage(Image avatarToChange,string where)
	{
		if (intialImage != avatar.sprite) {
//			PlayerPrefs.SetString("user", "promel");
			StartCoroutine(ImageUploader.UploadPNG(SpriteToTexture2D.Convert(avatar.sprite),table,where,null,avatarToChange,avatar.sprite));
			Debug.Log ("not same");
		} else 
		{
			Debug.Log ("same");
		}
	}
}