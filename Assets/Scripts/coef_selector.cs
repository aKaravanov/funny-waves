using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class coef_selector : MonoBehaviour {

	public GameObject changer;
	public Slider sld;

	public void Sin(){
		this.gameObject.SetActive (false);
		FindObjectOfType<line_user> ().chg_n = 0;
		sld.value = (FindObjectOfType<line_user> ().cons[0, 1] + 2.5F) / 5F;
		changer.SetActive(true);
	}

	public void Cos(){
		this.gameObject.SetActive (false);
		FindObjectOfType<line_user> ().chg_n = 1;
		sld.value = (FindObjectOfType<line_user> ().cons[1, 1] + 2.5F) / 5F;
		changer.SetActive(true);
	}

	public bool isAlive(){
		return this.isAlive ();
	}
}
