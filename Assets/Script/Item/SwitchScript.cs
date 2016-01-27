using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SwitchScript : MonoBehaviour {

	public string switchName;

	bool switchOnce;

	public GameObject windy;
	NavMeshAgent windyAgent;

	void Awake ()
	{
		windyAgent = windy.GetComponent<NavMeshAgent> ();
	}

	void OnEnable ()
	{
		EventManager.StartListening ("SwitchOn", SwitchOn);
	}

	void OnDisable () 
	{
		EventManager.StopListening ("SwitchOn", SwitchOn);
	}

	void SwitchOn ()
	{

		if (!switchOnce) {
			switchOnce = true;

			if (switchName == "level1Up") {
				StartCoroutine ("Level1Up");
				Debug.Log ("Level 1 Up");

			}
			else if (switchName == "level1Rotation") {

				StartCoroutine ("Level1Rotation");
			}
		}

	}

	IEnumerator Level1Up ()
	{
		//Debug.Log ("Level 1 Up");
		yield return new WaitForSeconds (.5f);
		if (GameObject.Find ("Level1WindPassage1")) {
			GameObject.Find ("Level1WindPassage1").SetActive (false);
		}
		//Debug.Log (4);
		GameObject.Find ("Level1WindyPassage1Slice4").transform.DOMove (new Vector3 (9, 7.4f, -8), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (3);
		GameObject.Find ("Level1WindyPassage1Slice3").transform.DOMove (new Vector3 (9, 7.4f, -6), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (2);
		GameObject.Find ("Level1WindyPassage1Slice2").transform.DOMove (new Vector3 (9, 7.4f, -4), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (1);
		GameObject.Find ("Level1WindyPassage1Slice1").transform.DOMove (new Vector3 (9, 7.4f, -2), 2);
		windyAgent.areaMask = 33;
	}

	void Level1Rotation ()
	{
		Debug.Log ("Level 1 Rotation");
		GameObject.Find ("Level1Rotation").transform.DORotate (new Vector3 (0, -270, 0), 2, RotateMode.FastBeyond360);
	}
}
