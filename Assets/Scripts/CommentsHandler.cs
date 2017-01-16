using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;

public class CommentsHandler : MonoBehaviour {
	private float buttonWidth;
	private float buttonHeight;
	public GameObject prefab;
	private GameObject comment_item;
	public Text commentText;
	public string orderBy;
	public string where;
	string account;
	string search;
	// Use this for initialization
	public static Comment[] comments;
	public Text errorMessage;

	public void LoadComments()
	{
		/*RectTransform myRect = GetComponent<RectTransform>();
		buttonHeight = myRect.rect.height;
		buttonWidth = myRect.rect.width;
		GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
		grid.cellSize = new Vector2(buttonWidth, buttonHeight);*/
		StartCoroutine(GetComments());
	}

	public void CreateComment()
	{
		StartCoroutine(SendRequest());
	}

	public IEnumerator GetComments()
	{
		DestroyComments();
		WWW download;
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("event_id", EventID.eventId);

		// Create a download object
		download = new WWW(UniversalURL.url + "read_comments.php", form);

		// Wait until the download is done
		yield return download;

		if (!string.IsNullOrEmpty(download.error))
		{
			EnableErrorMessage("No Connection");
		}
		else
		{
			if(download.text.Length>0)
			{
//				Debug.Log (download.text);
				PopulateComments(download.text);
			}
		}
	}


	public IEnumerator SendRequest()
	{
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField("table", "comments");
		form.AddField("event_id", EventID.eventId);
		form.AddField("username", PlayerPrefs.GetString("user"));
		form.AddField("comment", commentText.text);
		//form.AddField("user_type", PlayerPrefs.GetString("accountType"));

		// Create a download object
		WWW download = new WWW(UniversalURL.url + "insert.php", form);

		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			//print( "Error downloading: " + download.error );
			EnableErrorMessage("No Connection");
		} else {
			// show the highscores
			//Debug.Log(download.text);
			if (download.text.Length == 0)
			{
				string timeAndDate =  System.String.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
				comment_item = Instantiate(prefab);//.GetComponent<Button>());
				comment_item.transform.SetParent(transform, false);
				comment_item.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("user");
				comment_item.transform.GetChild(1).GetComponent<Text>().text = commentText.text;
				comment_item.transform.GetChild(2).GetComponent<Text>().text = timeAndDate;
				if(GameObject.FindGameObjectWithTag("avatar").GetComponent<Image>().sprite != null)
				{
					comment_item.transform.GetChild(3).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("avatar").GetComponent<Image>().sprite;
				}
			}
			else
			{
				//Error message on UI put text as param
				EnableErrorMessage(download.text);
			}
		}


	}

	public void PopulateComments(string jsonString)
	{
		//Debug.Log(jsonString);
		string[] jsonArray = jsonString.Split('|');
		//comments = new Comment[jsonArray.Length];
		for(int i = 0; i <jsonArray.Length;i++)
		{
	//		comments[i] = Comment.GetComment(jsonArray[i]);
			CreateCommentHelper(Comment.GetComment(jsonArray[i]));
		}
	}

	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}
	public void CreateCommentHelper(Comment comment)
	{
		comment_item = Instantiate(prefab);//.GetComponent<Button>());
		comment_item.transform.SetParent(transform, false);

		comment_item.transform.GetChild(0).GetComponent<Text>().text = comment.username;//PlayerPrefs.GetString("user");
		comment_item.transform.GetChild(1).GetComponent<Text>().text = comment.comment;//GameObject.FindGameObjectWithTag("comment").GetComponent<Text>().text;
		comment_item.transform.GetChild(2).GetComponent<Text>().text = comment.comment_timestamp;
		if(comment.explorer_avatar != null)
		{
			StartCoroutine(ImageDownloader.DownloadImage("explorers/" + comment.explorer_avatar,comment_item.transform.GetChild(3).gameObject));
		}
	}

	void DestroyComments()
	{
		foreach(GameObject comment in GameObject.FindGameObjectsWithTag("comment"))
		{
			Destroy(comment);
		}
	}
}
