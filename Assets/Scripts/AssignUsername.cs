﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AssignUsername : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = AccountManager.user;
	}
}
