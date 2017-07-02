using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

	static string ReadString()
	{
		string path = "Assets/Quiz/questions.csv";

		StreamReader reader = new StreamReader(path);
		string ret = reader.ReadToEnd();

		reader.Close();

		return ret;
	}

	struct Question
	{
		public string quest, aans, bans, cans, correct;
		public Question(string question, string aanswer, string banswer,
			string canswer, string correctanswer)
		{
			quest = question;
			aans = aanswer;
			bans = banswer;
			cans = canswer;
			correct = correctanswer;
		}

		public Question(string[] questionstring)
		{
			quest = questionstring[0];
			aans = questionstring[1];
			bans = questionstring[2];
			cans = questionstring[3];
			correct = questionstring[4];
		}

		string[] ToQuestionString()
		{
			string[] qs = new string[5];
			qs[0] = quest;
			qs[1] = aans;
			qs[2] = bans;
			qs[3] = cans;
			qs[4] = correct;

			return qs;
		}

	}

	List<Question> questions;
	public char actualanswer;
	float timeleft;

	public float quiztimer = 10.0f;

	bool ended = true;
	public bool Ended
	{
		get { return ended; }
		set { ended = value; }
	}

	void Start()
	{
		string temp = ReadString();

		string[] questionsstrings = temp.Split('\n');

		questionsstrings = questionsstrings.Skip(1).ToArray(); //labels

		questions = new List<Question>();

		foreach (var qs in questionsstrings)
		{
			questions.Add(new Question(qs.Split(';')));
		}

		gameObject.SetActive(false);
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		timeleft -= Time.deltaTime;
	}

	public void Show()
	{
		gameObject.SetActive(true);

		Text[] texts = gameObject.GetComponentsInChildren<Text>();

		int index = Random.Range(0, questions.Count - 1);

		texts[0].text = questions[index].quest;
		texts[1].text = questions[index].aans;
		texts[2].text = questions[index].bans;
		texts[3].text = questions[index].cans;

		actualanswer = questions[index].correct[0]; //should be one character actually

		timeleft = quiztimer;

		ended = false;

		questions.Remove(questions[index]);
	}
	
	public bool IsEnd()
	{
		if (timeleft <= 0 && !ended)
		{
			ended = true;

			gameObject.SetActive(false);

			return true;
		}

		return false;
	}

}
