using UnityEngine;
using System.Collections;

public class Linker : MonoBehaviour {
	[SerializeField]
	public void Link(string sceneToLoad) {
		Application.LoadLevel(sceneToLoad);
	}
}
