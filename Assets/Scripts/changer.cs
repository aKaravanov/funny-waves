using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changer : MonoBehaviour {

	public GameObject amp;

	public void Back(){
		this.gameObject.SetActive (false);
		amp.SetActive(true);
	}
}
