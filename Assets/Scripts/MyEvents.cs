using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MyEvents : MonoBehaviour
{
    private float buttonWidth;
    private float buttonHeight;
	public GameObject prefab;
    private Button button;
	public string orderBy;
	public string where;
	string account;
	string search;
//	public Sprite testSprite;
	public bool testing;
    void Start()
    {
		if(testing)
		{
			PlayerPrefs.SetString ("user","Just Tech");
			PlayerPrefs.SetString ("accountType", "promoter");
		}
		if(PlayerPrefs.GetString("accountType")!="promoter")
		{
			Application.LoadLevel("Menu");
		}
		account = "account ='" + PlayerPrefs.GetString("user") + "'";
		where = account;
		search="";
        StartCoroutine(GetMenuItems());
//		prefab.transform.GetChild(3).GetComponent<Image>().sprite = testSprite;

    }

    public void EventListHandler(string param)
    {
        RectTransform myRect = GetComponent<RectTransform>();
        buttonHeight = myRect.rect.height / 2;
        buttonWidth = myRect.rect.width;
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(buttonWidth, buttonHeight*8.8F);

        string[] rows = param.Split('#');
        //rows = data.Length;
		prefab.SetActive(true);
		//Button temp = prefab.GetComponent<Button>();
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
			string img = cols [cols.Length - 1];
			if(img !="")
			{
				Debug.Log (img);
				StartCoroutine(ImageDownloader.DownloadImage("events/thumbs/" + img, button.transform.GetChild(3).gameObject));
			}
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
		if(orderBy =="")
		{
			orderBy = "event_id";
		}
		form.AddField("order_by",orderBy);
		form.AddField("search", search);

        // Create a download object
		download = new WWW(UniversalURL.url + "read.php", form);

        // Wait until the download is done
        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
			Debug.Log("Error downloading: " + download.error);
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
		orderBy.transform.parent.gameObject.SetActive(false);
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
		category.transform.parent.gameObject.SetActive(false);
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
}