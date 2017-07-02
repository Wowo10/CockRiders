using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class levelscript : MonoBehaviour
{

	public PlayerBehaviour[] players;
	public Quiz quiz;

	public GameObject splashscreen;
	public AudioClip counting;

	public ParticleSystem clickerparticles;

	float delay;

	bool switchdelay = true;

	public GameObject[] backgroundlayers;

	public GameObject star1;
	public GameObject star2;

	public GameObject[] planets;
	float wintimer = 0;
	bool playerwon;

	//starting data
	GameObject[] QuizTriggers;
	Vector3[] Positions;

	void Start()
	{
		Positions = new Vector3[players.Length];

		QuizTriggers = GameObject.FindGameObjectsWithTag("QuizStart");
		for (int i = 0; i < players.Length; i++)
		{
			Positions[i] = players[i].transform.position;
		}

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

					int scale = Random.Range(2, 7);

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
		if (quiz.IsEnding())
		{
			foreach (var player in players)
			{
				player.CanVote = false;
			}
		}

		if (quiz.IsEnd())
		{
			foreach (var player in players)
			{
				if (player.currentanswer == quiz.actualanswer)
					player.WinQuiz();
				else
					player.LoseQuiz();

				player.CanVote = true;
			}

		}

		float delta = Time.deltaTime;

		wintimer -= delta;
		delay -= delta;

		if (playerwon && wintimer <= 0)
		{
			Restart();
		}


		if (delay <= 0 && !switchdelay)
		{
			foreach (var player in players)
			{
				player.InputDelay = false;
			}

			quiz.InputDelay = false;
		}

		for (int i = 0; i < players.Length; i++)
		{
			if (players[i].HasWon && !playerwon)
			{
				wintimer = 7.0f;
				playerwon = true;
				for (int j = 0; j < 4; j++)
				{
					ParticleSystem ps;

					ps = Instantiate(clickerparticles, players[i].transform);
					ps.transform.position = gameObject.transform.position;

					Destroy(ps.gameObject, ps.main.duration);
				}
			}
		}

	}

	void Restart()
	{
		splashscreen.SetActive(true);

		foreach (var item in QuizTriggers)
		{
			item.SetActive(true);
			for (int i = 0; i < players.Length; i++)
			{
				players[i].transform.position = Positions[i];
				players[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				players[i].HasWon = false;
			}
		}

		SceneManager.LoadScene("MainScene");
	}

	void Show()
	{
		quiz.Show();
	}

	public void SetStartDelay()
	{
		delay = 3.0f;
		switchdelay = false;

		//vo
		AudioSource audiosource;
		audiosource = GetComponent<AudioSource>();
		audiosource.PlayOneShot(counting);
	}

}
