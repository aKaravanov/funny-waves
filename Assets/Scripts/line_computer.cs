using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class line_computer : MonoBehaviour {

	// Game object of a line
	public LineRenderer line_c;
	// Distance between rendered points
	public float pointSpacing = 0.1f;
	// Redo this part // 
	// Scaling factor
	public int dx = -8;
	// Number of points in render
	public static int n_points = 100;
	// Array of y values
	public float[] y;

	void Start () {
		line_c = GetComponent<LineRenderer> ();
		line_c.material = new Material (Shader.Find ("Particles/Additive"));
		line_c.startColor = Color.grey;
		line_c.endColor = Color.grey;
		line_c.positionCount = n_points + 1;
		Draw (y);
	}

	public void Redraw(float [,] cons){
		Set_y (cons);
		Draw (y);
	}

	public void Set_y(float [,] cons){
		y = new float[n_points + 1];
		for (int x = 0; x <= n_points; x++) {
			y [x] = cons [0, 0] * cons [0, 1] * Mathf.Sin ((float)x * pointSpacing + cons [0, 2]) + cons [1, 0] * cons [1, 1] * Mathf.Cos ((float)x * pointSpacing + cons [1, 2]);
		}
	}

	public void Draw(float[] y_in){
		for (int x = 0; x <= n_points; x++) {
			line_c.SetPosition (x, new Vector3(x * pointSpacing + dx, y_in[x], 1));
		}
	}
}

