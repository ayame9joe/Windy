using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LaserSpawnScript : MonoBehaviour {

	public float spawnRate = 1.0f;

	float lastSpawnTime;
	float currentTime;

	public GameObject laser;
	
	// Use this for initialization
	void Start () {
		//m_laser = new GameObject ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Shoot ();

	}

	void Shoot () {
		currentTime = Time.time;

		if ((currentTime - lastSpawnTime) > spawnRate) {
			GameObject m_laser = GameObject.Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
			m_laser.transform.parent = this.transform;
			lastSpawnTime = currentTime;
		}

	}
}
