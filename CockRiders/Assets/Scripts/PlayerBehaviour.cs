using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public int playernumber;
	public float speed = 10.0f;
	public float friction = 0.05f;
	public float maxvelocity = 10.0f; //times that max velocity is bigger that speed
	public float startvelocity = 0.0f;
	public char currentanswer = '0';
	public float jumpHeight = 20.0f;
	public float jumpForce = 10.0f;
	public float animationOffset = 30;

	public ParticleSystem particles;
	public ParticleSystem clickerparticles;
	public AudioClip collisionSound;
	public AudioClip powerupcock;

	public float groundLevelY;
	public float knockedRotation = 30.0f;

	private bool isKnocked = false;
	private float maxVelocityTemp;
	private GameController controller;
	public float knockedTimer = 1.0f;
	public bool isResetingLerp;

	bool inputdelay = true;
	public bool InputDelay
	{
		get { return inputdelay; }
		set { inputdelay = value; }
	}

	bool haswon = false;
	public bool HasWon
	{
		get { return haswon; }
		set { haswon = value; }
	}

	bool canvote = true;

	public bool CanVote
	{
		get { return canvote; }
		set { canvote = value; }
	}

	string fire, jump, aans, bans, cans; //axes
	Rigidbody2D rb;

	void Start()
	{
		controller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
		groundLevelY = transform.position.y;
		fire = "Fire" + (playernumber + 1);
		jump = "Jump" + (playernumber + 1);
		aans = "Aanswer" + (playernumber + 1);
		bans = "Banswer" + (playernumber + 1);
		cans = "Canswer" + (playernumber + 1);

		rb = gameObject.GetComponent<Rigidbody2D>();

		maxvelocity = speed * maxvelocity;
		maxVelocityTemp = maxvelocity;
		rb.velocity = gameObject.transform.right * 0 * speed;
	}

	void Update()
	{

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("FinishLine"))
		{
			haswon = true;
			controller.isGameStarted = false; //set game finished
		}
		if (collision.CompareTag("Obstacle"))
		{
			if (rb.velocity.y <= 0.0f + Mathf.Epsilon)
				OnObstacleHit();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("QuizStart"))
		{
			GameObject.FindGameObjectWithTag("MainCamera").SendMessage("Show"); //showing quiz

			collision.gameObject.SetActive(false);

			foreach( var item in GameObject.FindObjectsOfType<PlayerBehaviour>())
			{
				item.CanVote = true;
			}
		}
	}

	private void OnObstacleHit()
	{
		isKnocked = true;
		Invoke("ResetRotation", 1.0f);
		maxVelocityTemp = 0.5f * maxvelocity;
        AudioSource audiosource;
        audiosource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        audiosource.PlayOneShot(collisionSound);

    }

	private void ResetRotation()
	{
		isKnocked = false;
		isResetingLerp = true;
		maxVelocityTemp = maxvelocity;
	}

	void FixedUpdate()
	{
		//only ticks if game is started (no one has won yet)
		if (controller.isGameStarted)
		{
			if (Input.GetButtonDown(fire))
			{
				if (!inputdelay)
				{
					float brake = 1 - (rb.velocity.x / maxVelocityTemp);
					rb.velocity += Vector2.right * speed * brake;
				}

				if (particles != null)
				{
					ParticleSystem ps;

					ps = Instantiate(particles);
					ps.transform.position = gameObject.transform.position;

					Destroy(ps.gameObject, ps.main.duration);
				}
				if (!isKnocked)
				{
					Animate();
				}
			}
			else
			{
				rb.velocity += Vector2.right * speed * friction * -1;

				if (rb.velocity.x < 0)
					rb.velocity = new Vector3(0, rb.velocity.y, 0);
			}

			if (Input.GetButtonDown(jump))
			{
				if (!inputdelay)
				{
					if (rb.velocity.y >= 0.0f - Mathf.Epsilon && rb.velocity.y <= 0.0f + Mathf.Epsilon)
						OnJump();
				}
			}
			if (canvote)
			{
				if (Input.GetButtonDown(aans))
				{
					currentanswer = 'a';
				}

				if (Input.GetButtonDown(bans))
				{
					currentanswer = 'b';
				}

				if (Input.GetButtonDown(cans))
				{
					currentanswer = 'c';
				}
			}

			if (Input.GetAxis("Cheat") > 0)
			{
				rb.velocity = new Vector3(1500, 0, 0);
			}
		}
		if (transform.position.y > groundLevelY)
		{
			rb.AddForce(new Vector2(0, -30.81f), ForceMode2D.Force);
		}
		else if (transform.position.y < groundLevelY)
		{
			transform.SetPositionAndRotation(new Vector3(transform.position.x, groundLevelY, transform.position.z), Quaternion.identity);
			rb.velocity = new Vector3(rb.velocity.x, 0, 0); ;
		}


		if (isKnocked)
		{
			knockedTimer -= Time.deltaTime;
			if (knockedTimer < 0.0f)
			{
				isKnocked = false;
				isResetingLerp = true;
			}
			transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, knockedRotation / 3, knockedRotation)), Quaternion.identity, knockedTimer);
		}
		if (isResetingLerp)
		{
			if (knockedTimer < 1.0f)
				knockedTimer += Time.deltaTime;
			else
			{
				knockedTimer = 1.0f;
				isResetingLerp = false;
			}
			transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, knockedRotation / 3, knockedRotation)), Quaternion.Euler(new Vector3(0,0, 375)), knockedTimer);
		}

	}

	public void WinQuiz()
	{
		currentanswer = '0';

		//bonus
		rb.velocity += Vector2.right * speed * 5;

		ParticleSystem ps;

		ps = Instantiate(clickerparticles,gameObject.transform);
		ps.transform.position = gameObject.transform.position;

		AudioSource audiosource;
		audiosource = GetComponent<AudioSource>();
		audiosource.PlayOneShot(powerupcock);

		Destroy(ps.gameObject, ps.main.duration);
		canvote = true;
	}

	public void LoseQuiz()
	{
		currentanswer = '0';
		canvote = true;
	}

	private void OnJump()
	{
		rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
	}

	private void Animate()
	{
		animationOffset *= -1;
		transform.rotation = Quaternion.Euler(0, 0, animationOffset);
	}

}
