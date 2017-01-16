using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;


public class Favourites : MonoBehaviour
{
	private float buttonWidth;
	private float buttonHeight;
	public GameObject prefab;
	private Button button;
	public string orderBy;
	public string where;
	string account;
	string search;
	public Text errorMessage;

	void Start()
	{
		account = "account ='" + PlayerPrefs.GetString("user") + "'";
		where = account;
		search="";
		StartCoroutine(GetMenuItems());
	}

	public void EventListHandler(string param)
	{
		RectTransform myRect = GetComponent<RectTransform>();
		buttonHeight = myRect.rect.height / 2;
		buttonWidth = myRect.rect.width;
		GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
		grid.cellSize = new Vector2(buttonWidth, buttonHeight*15.5f);

		string[] rows = param.Split('|');
		//rows = data.Length;
		//Button temp = prefab.GetComponent<Button>();
		GlobalData.ResetGlobalData();
		prefab.SetActive(true);
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
		form.AddField("username", PlayerPrefs.GetString("user"));

		// Create a download object
		download = new WWW(UniversalURL.url + "load_favourites.php", form);

		// Wait until the download is done
		yield return download;

		if (!string.IsNullOrEmpty(download.error))
		{
			EnableErrorMessage("No Connection");
		}
		else
		{
			Debug.Log(download.text);
			if(download.text.Length>0)
			{
				EventListHandler(download.text);
				Debug.Log(download.text);
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
				where += " AND event_category ='" + category.name + "'" ;
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
				where += " AND event_name";
			this.search = search.text;
			GlobalData.ResetGlobalData();
			DeleteExistingMenuItems();
			StartCoroutine(GetMenuItems());
		}
	}
	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}
}