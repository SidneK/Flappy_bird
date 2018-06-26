using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
	public AudioSource Fly;
	public AudioSource Hit;
	public AudioSource Point;
	public Text PrintScore;
	public int SpeedSpace;

	private float SpawnX;
	private float SpawnY;
	private Rigidbody2D rb;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezePositionY;
		SpawnX = rb.transform.position.x;
		SpawnY = rb.transform.position.y;
	}

	void Update()
	{
		if (Logic.Instance.IsPressed && !Logic.Instance.GameOver) // fly
		{
			Fly.Play();
			rb.AddForce(Vector2.up * SpeedSpace, ForceMode2D.Impulse);
			Logic.Instance.IsPressed = false;
		}
		if (Logic.Instance.IsPressed && Logic.Instance.GameOver) // begin
		{
			rb.Sleep(); // after addForce - normalizes
			PrintScore.text = "0";
			Logic.Instance.Score = 0;
			Logic.Instance.GameOver = false;
			Logic.Instance.PrintScore.enabled = false;
			GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
			GameObject[] Points = GameObject.FindGameObjectsWithTag("Point");
			foreach (GameObject it in Blocks)
				Destroy(it.gameObject);
			foreach (GameObject it in Points)
				Destroy(it.gameObject);
			rb.transform.position = new Vector3(SpawnX, SpawnY, 0);
			rb.transform.rotation = Quaternion.identity;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Point") // Print score
		{
			Destroy(collision.gameObject);
			Point.Play();
			++Logic.Instance.Score;
			PrintScore.text = Logic.Instance.Score.ToString();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (!Logic.Instance.GameOver && 
			(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Destroy"))
		{
			Hit.Play();
			rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
			rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
			Logic.Instance.GameOver = true;
			if (Logic.Instance.Score > Logic.Instance.Record)
			{
				PlayerPrefs.SetInt("Record", Logic.Instance.Score);
				PlayerPrefs.Save();
			}
			Logic.Instance.Record = PlayerPrefs.GetInt("Record");
			PrintScore.text = "Game Over\r\nScore: " + PrintScore.text + 
							"\r\nRecord: " + Logic.Instance.Record.ToString();
			Logic.Instance.PrintScore.enabled = true;
			Logic.Instance.IsStart = false;
		}
	}
}
