using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SideMenu : MonoBehaviour
{

    //refrence for the pause menu panel in the hierarchy
    public GameObject pauseMenuPanel;
    //animator reference
    private Animator anim;
    //variable for checking if the game is paused 
    public bool isfading = false;

	public GameObject SlideOutButton;
    // Use this for initialization
    void Start()
    {
        //unpause the game on start
        //get the animator component
        anim = pauseMenuPanel.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {
        //  info.length
       /* if (Input.GetKeyUp(KeyCode.Escape) && !isfading)
        {
               PauseGame();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isfading)
        {
            UnpauseGame();
        }*/

		/*if (!this.anim.GetCurrentAnimatorStateInfo (0).IsName ("SideMenuRight")) {
			Debug.Log ("pausing");
			PauseGame ();
		} else if(this.anim.GetCurrentAnimatorStateInfo (0).IsName ("SideMenuLeft")){
			Debug.Log ("unpausing");
			UnpauseGame ();
		
		}*/
    }

    //function to pause the game
    public void FadeIn()
    {
        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("SideMenuRight");
        //set the isPaused flag to true to indicate that the game is paused
        isfading = true;
		SlideOutButton.SetActive (true);

	/*float tempCol = SlideOutButton.GetComponent<Image> ().color.a;
		if(!isfading)
		{
			tempCol = Mathf.Lerp(tempCol, 0, Time.deltaTime * 2.0f);
		}
		else
		{

			tempCol = Mathf.Lerp(tempCol, 1, Time.deltaTime * 2.0f);
		}  */
    }
    //function to unpause the game
	public void FadeOut()
    {
        //set the isPaused flag to false to indicate that the game is not paused
        isfading = false;
        //play the SlideOut animation
        anim.Play("SideMenuLeft");
		SlideOutButton.SetActive (false);
    }
}


