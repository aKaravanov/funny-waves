using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_manager : MonoBehaviour {

	// User's line
	public line_user user;
	// Computer's line
	public line_computer comp;
	// Maximum error 
	public float err;
	// Maximum error 
	public float term_id;
	// Degree of trig function
	private static int degree = 1;
	// Constant for the max error
	public static float max_error = 0.2F;
	// Figure out how to import from other class //
	public static int n_points = 100;
	// Victory menu
	public victory_menu v_menu;
	private static string[] words = {"almost got it!", "hotter", "hot", "cold", "colder", "Arctica!"};

	// Creating matrices of constants
	// Each row coresponds to a speecific trigonometric function, which can be described by a formula
	// y = a*b*sin(x+c)
	// [a,0] corresponds to the first amplitude multiplier
	// [a,1] corresponds to the second amplitude multiplier
	// [a,2] corresponds to the phase difference 
	// [a,3] corresponds to the stored value of the correct amplitude
	private float[,] cons_u = new float[degree * 2,4];
	private float[,] cons_c = new float[degree * 2,4];
    // Camera object
    private Camera cm;
    // Determine whether the game is on
    private bool inGame;

	// Use this for initialization
	void Awake () {
		
		// Initiates and sets users line to a graphical object of type line_user
		user = FindObjectOfType<line_user> ();
		// Initiates and sets users line to a graphical object of type line_user
		comp = FindObjectOfType<line_computer> ();
        // Initiates and sets camera object
        cm = Camera.main.GetComponent<Camera>();

		// Sets the parameters for the sin(x) function
		// Sets the initial amplitude to be one
		cons_u [0, 0] = 1F;
		// Choses randomly the initial amplitude
		cons_u[0, 1] = 5 * Random.value - 2.5F;
		// Choses randomly the initial phase difference
		cons_u[0, 2] = 5 * Random.value - 2.5F;
		// Stores the initial amplitude
		cons_u[0, 3] = cons_u[0, 1];

		// Sets the parameters for the cos(x) function
		// Sets the initial amplitude to be one
		cons_u[1, 0] = 1F;
		// Choses randomly the initial amplitude
		cons_u[1, 1] = 5 * Random.value - 2.5F;
		// Choses randomly the initial phase difference
		cons_u[1, 2] = 5 * Random.value - 2.5F;
		// Stores the initial amplitude
		cons_u[1, 3] = cons_u[1, 1];

		// Sets the value of slider to the stored value of the amplitude
		// Look up into this //
		user.mainSlider.value = (cons_u[0, 1] + 2.5F) / 5F;

		// Sets up the computer line
		user.Set_y (cons_u);

		// Creates a copy of a constant matix
		for (int i = 0; i < degree * 2; i++){
			for (int j = 0; j < 4; j++){
				cons_c[i,j] = cons_u[i,j]; 
			}
		}
			
		// Choses the constants for the computer's graph that would not differ from the 
		// previously choosen constants for more then one.
		while (Mathf.Abs (cons_c [0, 1] - cons_u [0, 1]) < 1) {
			while (Mathf.Abs (cons_c [0, 2] - cons_u [0, 2]) < 1) 
				cons_c [0, 2] = 5 * Random.value - 2.5F;
			cons_c [0, 1] = 5 * Random.value - 2.5F;
		}

		// Sets up the computer line
		comp.Set_y (cons_c);
	}
	
	// Update is called once per frame
	void Update () {
		err = MaxDif (comp, user);
        ChangeBackground(err);
		if (err < max_error | Input.GetKeyDown (KeyCode.Space)) {
			v_menu.Victory ();
			Randomize();
            cm.backgroundColor = new Color(0f, 1f, 0f, 1f);
			user.cons = cons_u;
			user.mainSlider.value = (cons_u[0, 1] + 2.5F) / 5F;
			user.Redraw (cons_u);
			comp.Redraw (cons_c);
			FindObjectOfType<changer> ().Back ();
		}
	}

	// Chooses new coefficients
	public void Randomize(){
		for (int i = 0; i < degree * 2; i++) {
			cons_u [i, 0] = 1F;
			cons_u [i, 1] = 5 * Random.value - 2.5F;
			cons_u [i, 2] = 5 * Random.value - 2.5F;
			cons_u [i, 3] = cons_u[i, 1];
			for (int j = 0; j < 4; j++)
				cons_c [i, j] = cons_u [i, j];
		}
			
		while (Mathf.Abs (cons_c [0, 1] - cons_u [0, 1]) < 1) {
			while (Mathf.Abs (cons_c [1, 1] - cons_u [1, 1]) < 1) 
				cons_c [1, 1] = 5 * Random.value - 2.5F;
			cons_c [0, 1] = 5 * Random.value - 2.5F;
		}
	}

	public float MaxDif(line_computer line_comp_temp, line_user line_user_temp){
		float max = -1F;
		term_id = -1;
		float temp = max;
		for (int i = 3; i < n_points - 1; i++){
			temp = Mathf.Abs(line_user_temp.y[i] - line_comp_temp.y[i]);
			if (temp > max) {
				max = temp;
				term_id = i;
			}
		}
		return max;
	}

	// Sets background color based on the answer
    public void ChangeBackground(float err)
    {   
        float p_error = (max_error + 0.7f) / (err + 0.7f);
        Debug.Log(err + " / " + p_error);
        cm.backgroundColor = new Color(1 - p_error, p_error, 0f, 1f);
	}
}


