using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class EventListController : MonoBehaviour
{
    public int rows = 2;
    public int columns = 2;
    private float buttonWidth;
    private float buttonHeight;
    public Button prefab;
    public Button AddEvent;
    private Button button;
    private Button _addEvent;
    public Button[] EventsTabs;
    public Button Delete;
    void Start()
    {
        EventListHandler();

        Delete = GameObject.Find("Delete Button").GetComponent<Button>();
        //EnableDeleteButton ();
    }

    public void EventListHandler()
    {
        RectTransform myRect = GetComponent<RectTransform>();
        buttonHeight = myRect.rect.height / 2;
        buttonWidth = myRect.rect.width / 2;
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(804, buttonHeight);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                button = (Button)Instantiate(prefab);
                button.transform.SetParent(transform, false);
                //EventsTabs [j] = button;

                button.onClick.AddListener(EnableDeleteButton);
            }

            if (GameObject.Find("My Events Title"))
            {
                _addEvent = (Button)Instantiate(AddEvent);
                _addEvent.transform.SetParent(transform, false);
                _addEvent.onClick.AddListener(LoadScene);
            }
        }
    }

    void LoadScene()
    {
        GameObject.Find("_Controller").GetComponent<Linker>().Link("New Event");
    }

    void EnableDeleteButton()
    {
        Delete.GetComponent<Image>().enabled = true;
    }
}