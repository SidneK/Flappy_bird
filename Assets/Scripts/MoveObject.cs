using UnityEngine;

public class MoveObject : MonoBehaviour
{
	public float Speed;

	private Rigidbody2D rb;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();	
	}

	void Update()
	{
		if (!Logic.Instance.GameOver && !Logic.Instance.IsPause)
			rb.transform.position = new Vector3(rb.transform.position.x - Speed,
											rb.transform.position.y, rb.transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "DestroyBlock")
			Destroy(gameObject);
	}
}