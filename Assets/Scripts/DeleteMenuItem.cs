using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeleteMenuItem : MonoBehaviour
{
	public Text errorMessage;
	public void Start()
	{
	}
    public void Delete()
    {
        StartCoroutine(DeleteRequest());//the name of the object is it ID 
    }

    public IEnumerator DeleteRequest()
    {
        WWW download;
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();
        // Assuming the perl script manages high scores for different games
		string table = name.Split('_')[1];
		form.AddField("table", table);
		form.AddField("event_id", transform.parent.name);

        // Create a download object
		download = new WWW(UniversalURL.url + "delete.php", form);

        // Wait until the download is done
        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
//            print("Error downloading: " + download.error);
			EnableErrorMessage("No Connection");
        }
        else
        {
			if(download.text.Length == 0)
			{
				EnableErrorMessage(transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().text 
								    + "\n deleted successfully");
				Destroy(transform.parent.gameObject);
			}
			else
			{
				EnableErrorMessage(download.text);
			}
        }
    }
		
	void EnableErrorMessage(string text)
	{
		errorMessage.transform.parent.transform.parent.gameObject.SetActive(true);
		errorMessage.text = text;
	}
}

