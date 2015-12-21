using UnityEngine;
using System.Collections;

public class WindyScript : MonoBehaviour {

	public bool open;

	NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (open) {
			navMeshAgent.areaMask = 1;
		} else {
			navMeshAgent.areaMask = 9;
		}
	
	}
}
