﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory_menu : MonoBehaviour {

	public GameObject lines;
	public GameObject selector;
	public GameObject changer;
	public GameObject closness;

	public void Next () {
		this.gameObject.SetActive (false);
		lines.SetActive (true);
		selector.SetActive (true);
		closness.SetActive (true);
	}

	public void Victory() {
		lines.SetActive (false);
		selector.SetActive (false);
		changer.SetActive (false);
		closness.SetActive (false);
		this.gameObject.SetActive (true);
	}
		
}
