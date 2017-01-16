using UnityEngine;
using System.Collections;

public class VIPManager : MonoBehaviour {

	public GameObject[] ARButtons;

	public GameObject dummy;
	public GameObject ImageTarget;
	float scale = 0f;
	float minScale = 0;
	float maxScale = 0.03f;
	float scaleSpeed = 0.05f;

	// Use this for initialization

	void Start()
	{
		//Marker.GetComponent<Renderer>().enabled = false;
		for (int i = 0; i < ARButtons.Length; i++) {
			//ARButtons [i].SetActive (false);

		}
	}
	// Update is called once per frame
	void Update () 
	{
		scale += scaleSpeed * Time.deltaTime;
		// Limit the growth
		if (scale > maxScale) {
			scale = maxScale;
		}

		if (GameObject.Find ("ImageTarget")) 
		{
			StartCoroutine(ScaleUp());
		}
		// Apply the new scale

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			RaycastHit hit;

			if ( Physics.Raycast(ray, out hit) )
			{
				if (hit.transform.gameObject.tag == "Home") 
				{
					GetComponent<Linker> ().Link("VIP Offers EventList");
				}
				if (hit.transform.gameObject.tag == "Upload") {
					GetComponent<Linker> ().Link("New Event");
				}
				if (hit.transform.gameObject.tag == "Profile") {
					GetComponent<Linker> ().Link("New Event");
				}
				if (hit.transform.gameObject.tag == "VIP") {
					GetComponent<Linker> ().Link("VIP Offers EventList");

				}
			}
		}
		
	}


	IEnumerator ScaleUp() {


		for (int i = 0; i < ARButtons.Length; i++) {
			ARButtons[i].transform.LookAt(dummy.transform);
			ARButtons[i].transform.localScale = new Vector3(scale, scale, scale);
			yield return new WaitForSeconds(1);

		}
	}



}
