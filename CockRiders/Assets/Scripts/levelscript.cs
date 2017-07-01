using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelscript : MonoBehaviour {

    public PlayerBehaviour[] players;
    public Quiz quiz;
        
	void Start()
    {
        quiz.Show();
	}
	
	void Update()
    {
		if(quiz.IsEnd())
        {
            foreach (var player in players)
            {
                //Debug.Log(player.name + " " + player.currentanswer);

                if (player.currentanswer == quiz.actualanswer)
                    player.WinQuiz();
                else
                    player.LoseQuiz();
            }
        }
	}
}
