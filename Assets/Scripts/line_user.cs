using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class line_user : MonoBehaviour {

	// Game object of a line
	public LineRenderer line_u;
	// Stores constants
	public float[,] cons;
	// Game object of the slider that changes values of amplitudes
	public Slider mainSlider;
	// Distance between rendered points
	public float pointSpacing = 0.1f;
	// Redo this part // 
	// Scaling factor
	public int dx = -8;
	// Number of points in render
	public static int n_points = 100;
	// Array of y values
	public float[] y;
	// Constant that shows which value is changing
	public int chg_n;

	// Use this for initialization
	void Start () {
		chg_n = 0;
		line_u = GetComponent<LineRenderer> ();
		line_u.material = new Material (Shader.Find ("Particles/Additive"));
		line_u.startColor = Color.white;
		line_u.endColor = Color.white;
		line_u.positionCount = n_points + 1;
		Draw (y);
	}
		
	void Update () {
		StartCoroutine (graphCo ());
	}

	IEnumerator graphCo(){
		yield return new WaitForSecondsRealtime (0.01f);
		mainSlider.onValueChanged.AddListener (ListenerMethod);



		if (cons[chg_n, 1] != cons[chg_n, 3]) {
			if (Mathf.Abs (cons[chg_n, 1] - cons[chg_n, 3]) > 0.1) {
				if (cons[chg_n, 1] > cons[chg_n, 3]) {
					for (float i = cons[chg_n, 1]; i >= cons[chg_n, 3]; i--) {
						cons[chg_n, 1] = i;
						Redraw (cons);
						Time.timeScale = 0f;
						yield return new WaitForSecondsRealtime (0.01F);
						Time.timeScale = 1f;
					} 
					cons[chg_n, 1] = cons[chg_n, 3];
				} else {
					for (float i = cons[chg_n, 1]; i <= cons[chg_n, 3]; i++ ){
						cons[chg_n, 1] = i;
						Redraw (cons);
						Time.timeScale = 0f;
						yield return new WaitForSecondsRealtime (0.01F);
						Time.timeScale = 1f;
					}
					cons[chg_n, 1] = cons[chg_n, 3];
				}
			}
		}
	}

	public void ListenerMethod(float value){
		cons[chg_n, 3] = 5 * value - 2.5F;
	}

	public void Set_y(float [,] cons_in){
		y = new float[n_points + 1];
		for (int x = 0; x <= n_points; x++) {
			y [x] = cons_in [0, 0] * cons_in [0, 1] * Mathf.Sin ((float)x * pointSpacing + cons_in [0, 2]) + cons_in [1, 0] * cons_in [1, 1] * Mathf.Cos ((float)x * pointSpacing + cons_in [1, 2]);
		}
		cons = cons_in;
	}

	public void Draw(float[] y_in){
		for (int x = 0; x <= n_points; x++) {
			line_u.SetPosition (x, new Vector3(x * pointSpacing + dx, y_in[x], 1));
		}
	}

	public void Redraw(float [,] cons_in){
		Set_y (cons_in);
		Draw (y);
	}
}
