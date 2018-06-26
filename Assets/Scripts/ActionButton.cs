using UnityEngine;

public class ActionButton : MonoBehaviour
{
	public GameObject PausePop;
	public GameObject SpawnPausePop;

	void OnMouseDown()
	{
		if (gameObject.tag == "Resume")
		{
			Destroy(GameObject.FindGameObjectWithTag("PausePop"));
			Time.timeScale = 1;
			Logic.Instance.IsPause = false;
		}
		if (gameObject.tag == "Exit")
			Application.Quit();
	}

	public void UseButton(string Name)
	{
		if (Name == "Pause")
		{
			Instantiate(PausePop, SpawnPausePop.transform.position, Quaternion.identity);
			Time.timeScale = 0;
			Logic.Instance.IsPause = true;
		}
	}
}
