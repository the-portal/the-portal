using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EventListMenu : MonoBehaviour
{
    public int rows = 2;
    public int columns = 2;
    private float buttonWidth;                                        
    private float buttonHeight;                                       
    public Button prefab;
    private Button button;
	public GameObject eventList;
	public GameObject DialogMenu;


	void Start()
    {
	//	StartCoroutine (Example());
        EventListHandler();
    }

	public  void EventListHandler( )
    {
        RectTransform myRect = GetComponent<RectTransform>();        
        buttonHeight = myRect.rect.height / 2;            
        buttonWidth = myRect.rect.width / 2;           
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(buttonWidth, buttonHeight);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                button = (Button)Instantiate(prefab);
                button.transform.SetParent(transform, false);
				button.onClick.AddListener (enable);
            }
        }
    }

	public void enable()
	{
		DialogMenu.SetActive (true);
	}

	public void disable()
	{
		DialogMenu.SetActive (false);
	}

	/*IEnumerator Example()
	{
		gameObject = (GameObject)Instantiate(DialogMenu);
		gameObject.transform.SetParent(GameObject.Find ("Navigation").transform, false);
		yield return new  WaitForSeconds(1);
		gameObject.SetActive (true);
	}*/
}