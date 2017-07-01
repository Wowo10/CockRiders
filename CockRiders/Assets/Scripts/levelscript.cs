﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelscript : MonoBehaviour {

    public PlayerBehaviour[] players;
    public Quiz quiz;
        
	void Start()
    {

	}
	
	void Update()
    {
		if(quiz.IsEnd())
        {
            foreach (var player in players)
            {
                if (player.currentanswer == quiz.actualanswer)
                    player.WinQuiz();
                else
                    player.LoseQuiz();
            }
        }
	}

	void Show()
	{
		quiz.Show();
	}


}
