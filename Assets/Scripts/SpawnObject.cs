using UnityEngine;

public class SpawnObject : MonoBehaviour
{
	public GameObject Block;

	private float Seconds;
	private const float TimeSpawnObject = 1.3f;
	private Vector3 PositionObject;

	void Start()
	{
		Seconds = 0.0f;
		PositionObject = new Vector3(10.40f, Random.Range(-6.0f, 0.66f), 0.0f);
	}

	void Update()
	{
		if (!Logic.Instance.GameOver && !Logic.Instance.IsPause)
		{
			Seconds += Time.deltaTime;
			if (Seconds >= TimeSpawnObject)
			{
				Instantiate(Block, PositionObject, Quaternion.identity);
				PositionObject = new Vector3(10.40f, Random.Range(-6.0f, 0.66f), 0.0f);
				Seconds = 0.0f;
			}
		}
	}
}