using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
	public Text PrintScore;
	public Animator Background;
	public bool IsPressed;
	public bool IsPause;
	public bool IsStart;
	public bool GameOver;
	public int Score;
	public int Record;

	static public Logic Instance { get; private set; }
	void Awake()
	{
		Instance = this;
		Record = PlayerPrefs.GetInt("Record");
	}

	void Update()
	{
		if (!GameOver || IsStart) // play
			Background.StopPlayback();
		else if (GameOver) // stop
			Background.StartPlayback();
	}

	void OnMouseDown()
	{
		IsPressed = true;	
	}
}
