using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelscript : MonoBehaviour {

    public PlayerBehaviour[] players;
    public Quiz quiz;
        
	void Start () {
        quiz.Show();
	}
	
	void Update () {
		
	}
}
