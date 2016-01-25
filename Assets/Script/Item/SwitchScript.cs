using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SwitchScript : MonoBehaviour {

	public string switchName;

	bool switchOnce;

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
		
		if (switchName == "level1Up") {
			if (!switchOnce) {
				StartCoroutine ("Level1Up");
				switchOnce = true;
			}
		}
	}

	IEnumerator Level1Up ()
	{
		//Debug.Log ("Level 1 Up");
		yield return new WaitForSeconds (1);
		if (GameObject.Find ("Level1WindPassage1")) {
			GameObject.Find ("Level1WindPassage1").SetActive (false);
		}
		//Debug.Log (4);
		GameObject.Find ("Level1WindyPassage1Slice4").transform.DOMove (new Vector3 (9, 7.5f, -8), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (3);
		GameObject.Find ("Level1WindyPassage1Slice3").transform.DOMove (new Vector3 (9, 7.5f, -6), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (2);
		GameObject.Find ("Level1WindyPassage1Slice2").transform.DOMove (new Vector3 (9, 7.5f, -4), 2);
		yield return new WaitForSeconds (1);
		//Debug.Log (1);
		GameObject.Find ("Level1WindyPassage1Slice1").transform.DOMove (new Vector3 (9, 7.5f, -2), 2);
	}
}
