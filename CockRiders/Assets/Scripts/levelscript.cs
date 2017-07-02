using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class levelscript : MonoBehaviour {

    public PlayerBehaviour[] players;
    public Quiz quiz;

    float delay;

	bool switchdelay = true;

	public GameObject[] backgroundlayers;

	public GameObject star1;
	public GameObject star2;

	public GameObject[] planets;

	void Start()
    {
		foreach (var bglayer in backgroundlayers)
		{							
			for (int i = 0; i < 1600; i++)
			{
				GameObject temp = new GameObject();

				if (i % 100 == 0)
				{
					switch (Random.Range(0, 6))
					{
						case 0:
							temp = Instantiate(planets[0], bglayer.transform);
							break;
						case 1:
							temp = Instantiate(planets[1], bglayer.transform);
							break;
						case 2:
							temp = Instantiate(planets[2], bglayer.transform);
							break;
						case 3:
							temp = Instantiate(planets[3], bglayer.transform);
							break;
						case 4:
							temp = Instantiate(planets[4], bglayer.transform);
							break;
						case 5:
							temp = Instantiate(planets[5], bglayer.transform);
							break;
					}

					int scale = Random.Range(2,7);

					temp.transform.localScale = new Vector3(scale, scale, 0);
				}
				else
				{				
					if (Random.Range(0, 2) == 0)
					{
						temp = Instantiate(star1, bglayer.transform);
					}
					else
					{
						temp = Instantiate(star2, bglayer.transform);
					}
				}				

				float x = Random.Range(-300, 9000);
				float y = Random.Range(-105, 105);
				temp.transform.position = new Vector3(x, y, 0);
			}
		}

        AudioSource startaudio;
        startaudio = GetComponent<AudioSource>();
        //startaudio.Play();
        startaudio.Play(3 * 44100);
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

		delay -= Time.deltaTime;

		if(delay <= 0 && !switchdelay)
		{
			foreach (var player in players)
			{
				player.InputDelay = false;
			}

			quiz.InputDelay = false;
		}
	}

	void Show()
	{
		quiz.Show();
	}

	public void SetStartDelay()
	{
		delay = 3.0f;
		switchdelay = false;
	}

}
