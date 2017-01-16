using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using SimpleJSON	;
public class EventListManager : MonoBehaviour
{
	private float buttonWidth;
	private float buttonHeight;
	public GameObject prefab;
	private Button button;
	public string orderBy;
	public string where;
	string account;
	string search;
	public GameObject DialogMenu;
	public Text errorMessage;
	//public GameObject DialogMenu;
	void Start()
	{
		account = "";
		where = account;
		search="";
		StartCoroutine(GetMenuItems());
	}

	public void EventListHandler(string param)
	{
		RectTransform myRect = GetComponent<RectTransform>();
		buttonHeight = myRect.rect.height /2;
		buttonWidth = myRect.rect.width /2;
		GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
		grid.cellSize = new Vector2(buttonWidth, buttonHeight*10);

		string[] rows = param.Split('#');
		//rows = data.Length;
		prefab.SetActive(true);
		GlobalData.ResetGlobalData();
		for (int i = 0; i < rows.Length; i++)
		{
			int x = 0;
			string[] cols = rows[i].Split(',');

			GlobalData.eventsData.Add(cols[x],cols);
			button = Instantiate(prefab.GetComponent<Button>());
			button.transform.SetParent(transform, false);	
			button.tag = "menuItem";
			button.name = cols[x++];
			button.transform.GetChild(0).GetComponent<Text>().text = cols[x++];
			button.transform.GetChild(1).GetComponent<Text>().text = cols[x++];	
			button.transform.GetChild(2).GetComponent<Text>().text = cols[++x] + "," + cols[++x];
			button.onClick.AddListener (enable);
			//GlobalData.eventItems.Add(cols[0],button.transform.gameObject);

		}
		prefab.SetActive(false);
	}

	public IEnumerator GetMenuItems()
	{
		WWW download;
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table", "events");
		form.AddField("where", where);
		form.AddField("order_by", orderBy);
		form.AddField("search", search);

		// Create a download object
		download = new WWW(UniversalURL.url + "read.php", form);

		// Wait until the download is done
		yield return download;

		if (!string.IsNullOrEmpty(download.error))
		{
//			print("Error downloading: " + download.error);
			EnableErrorMessage("No Connection");
		}
		else
		{
			Debug.Log(download.text);
			if(download.text.Length>0)
			{
				EventListHandler(download.text);
			}
		}
	}

	public void OrderBy(GameObject orderBy)
	{
		this.orderBy = orderBy.name;
		GlobalData.ResetGlobalData();
		DeleteExistingMenuItems();
		StartCoroutine(GetMenuItems());
	}

	public void SortByGategory(GameObject category)
	{
		where = account;
		if(!category.name.Equals("All"))
		{
				where = "event_category ='" + category.name + "'" ;
		}	
		GlobalData.ResetGlobalData();
		DeleteExistingMenuItems();
		StartCoroutine(GetMenuItems());
	}

	public void DeleteExistingMenuItems()
	{
		GameObject[] menuItems = GameObject.FindGameObjectsWithTag("menuItem");
		foreach(GameObject menuItem in menuItems)
		{
			Destroy(menuItem);
		}
	}

	public void Search(Text search)
	{
		where = account;
		if(search.text != null)
		{		
				where += "event_name";
			this.search = search.text;
			GlobalData.ResetGlobalData();
			DeleteExistingMenuItems();
			StartCoroutine(GetMenuItems());
		}
	}

    public void enable()
    {
        DialogMenu.SetActive(true);
    }

    public void disable()
    {
        DialogMenu.SetActive(false);
    }

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}

}