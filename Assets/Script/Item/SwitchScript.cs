using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SwitchScript : MonoBehaviour {

	public string switchName;

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
			//Debug.Log ("Switch On");
			GameObject.Find ("Level1WindyPassage1").transform.DOMove (new Vector3 (9, 7.5f, -10), 2);

		}
	}
}
