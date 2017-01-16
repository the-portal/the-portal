using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

	[SerializeField]
	public void SetPopUpsTrue (GameObject popUps) {
		popUps.SetActive(true);
	}

	[SerializeField]
	public void SetPopUpsFalse (GameObject popUps) {
		popUps.SetActive(false);
	}
}
