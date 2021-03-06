﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

	Ray m_ray;
	RaycastHit m_hitInfo;
	public float m_rayDistance = 1000;

	LayerMask m_layerMask;
	LayerMask itemLayerMask;

	Vector3 desPos;

	NavMeshAgent m_agent;
	OffMeshLinkData linkData;

	public float minDis = 2f;

	public GameObject windy;
	NavMeshAgent windyAgent;

	int callingTimes;

	int boxTappingTimes;

	float energyMoveDuration = 2;

	float moveDuration = 2;

	int maxGridNum = 15;

	public GameObject walkingShadowAnim;

	public GameObject storyBoardPanel;



	// Use this for initialization
	void Start () {
		m_agent = this.GetComponent<NavMeshAgent> ();
		m_layerMask = 1 << 8;
		itemLayerMask = 1 << 9;
		windyAgent = windy.GetComponent<NavMeshAgent> ();

//		walkingShadowAnim = this.GetComponent<Animation> ();



	}
	
	// Update is called once per frame
	void Update () {

		//if (!storyBoardPanel.activeSelf) {
			Move ();
			TraverseOffMeshLink ();
			OnSwitchTap ();
			OnRotatedEnemyTap ();
			//OnCallingTap ();
			//OnCallingEnter ();
			OnCalling();
			OnDangerAlertTap ();
			OnBoxTap ();
			OnEnergyTap ();

		
		//}



	}

	void Move () {

		if (Input.GetMouseButtonDown (0)) {
			// 根据鼠标在屏幕空间的位置计算射线
			m_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			// 进行三维场景中的射线求交
			if (Physics.Raycast (m_ray, out m_hitInfo, m_rayDistance, m_layerMask)) {
				if (m_hitInfo.transform.tag == "Floor" || m_hitInfo.transform.tag == "WindThrough") {
					for (int i = -maxGridNum; i < maxGridNum; i++) {
						for(int j = -maxGridNum; j < maxGridNum; j++){
							for (int k = -maxGridNum; k < maxGridNum; k++){
								Vector3 tempVec3 = new Vector3(i * 2, j * 2 + transform.position.y * 1f, k * 2 + 1);
								if (Vector3.Distance(m_hitInfo.point, tempVec3) < 1f){
									desPos = tempVec3;
									this.transform.LookAt(desPos);
									m_agent.SetDestination(desPos);


									StartCoroutine("WalkingShadowGenerate");
								}
							}
						}
					}


					//Debug.Log (m_hitInfo.point);

				}
			}
		}


	}

	IEnumerator WalkingShadowGenerate(){

		GameObject go = GameObject.Instantiate(walkingShadowAnim, new Vector3(desPos.x + 0.5f, desPos.y, desPos.z + 0.5f), walkingShadowAnim.transform.rotation) as GameObject;
		//Debug.Log (Vector3.Distance(m_hitInfo.point, tempVec3) );
		
		
		
		/*AnimatorStateInfo info = walkingShadowAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

									// 判断动画是否播放完成
									if (info.nameHash == Animator.StringToHash("Base Layer.WalkingShadowAnim"))
									{
										if (info.normalizedTime >= 1f)
										{
											walkingShadowAnim.GetComponent<Animator>().SetBool("Exit", true);
										}
									}*/
		WalkingShadowGenerate();
		yield return new WaitForSeconds(1f);
		
		Destroy(go);
		
	}

	void TraverseOffMeshLink () {
		if (m_agent.isOnOffMeshLink) {
			linkData = m_agent.currentOffMeshLinkData;
			this.transform.DOMove(linkData.endPos, moveDuration);
		}
		if (Vector3.Distance(this.transform.position, linkData.endPos) < 0.2f){
			m_agent.Resume();
			m_agent.CompleteOffMeshLink();
		}
	}

	void OnSwitchTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Switch");
		for(int i = 0; i < gos.Length; i++) {
			//Debug.Log(Vector3.Distance(this.transform.position, gos[i].transform.position));
			if(Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray,out m_hitInfo, m_rayDistance, itemLayerMask))
				{
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							gos[i].GetComponent<MovableScript>().hasCheckedType = false;
						}
					}
				}
			}


			if (!gos[i].GetComponent<MovableScript>().hasCheckedType) {
				//Debug.Log ("Switch On");
				gos[i].GetComponent<MovableScript>().SwitchOn();
				gos[i].GetComponent<MovableScript>().CheckType();
				gos[i].GetComponent<MovableScript>().hasCheckedType = true;
			}
			else {

				//gos[i].GetComponent<MovableScript>().hasCheckedType = false;
				//Debug.Log ("CheckType");
			}
		}


	}

	void OnRotatedEnemyTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("RotatedEnemy");
		for (int i = 0; i < gos.Length; i++) {
			if (Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							//Debug.Log("RotateOn");
							gos[i].GetComponent<RotatedEnemyScript>().RotateOn();
						}
					}
				}
			}
		}
	}

	/*void OnCallingTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Calling");
		for (int i = 0; i < gos.Length; i++) {
			//Debug.Log(Vector3.Distance(this.transform.position, gos[i].transform.position));
			if (Input.GetMouseButtonDown(0)){

				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						Debug.Log("On Calling Tap");
						if (m_hitInfo.transform == gos[i].transform) {
							callingTimes++;
							if(callingTimes % 2 == 1){
								windyAgent.SetDestination(m_hitInfo.point);
							} else {
								windyAgent.SetDestination(windy.transform.position);
							}
						}
					}
				}
			}
		}
	}*/

	void OnDangerAlertTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("DangerAlert");
		for (int i = 0; i < gos.Length; i++) {
			if (Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							Debug.Log("On Danger Alert");
							windyAgent.areaMask -= 16;
						}
					}
				}
			}
		}
	}

	void OnBoxTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Box");
		for (int i = 0; i < gos.Length; i++) {
			if (Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							boxTappingTimes++;
							if(boxTappingTimes % 2 == 1){
								gos[i].transform.parent = this.transform;
							} else {
								gos[i].transform.parent = null;
							}
						}
					}
				}
			}
		}
	}

	void OnEnergyTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Energy");
		for (int i = 0; i < gos.Length; i++) {
			if (Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							Debug.Log("On Energy");
							gos[i].transform.DOMove(windy.transform.position, energyMoveDuration);
						}
					}
				}
			}
		}
	}


	void OnTriggerEnter(Collider other){
		//Debug.Log("On Calling Enter");
		/*if (other.tag == "Calling") {
			//Debug.Log("On Calling Enter");
			Vector3 tempPos = other.transform.position;
			//other.transform.position = new Vector3(tempPos.x, tempPos.y - 0.4f, tempPos.z);
			other.transform.DOMove(new Vector3(tempPos.x, tempPos.y - 0.3f, tempPos.z), moveDuration);
			windyAgent.SetDestination(tempPos);
		}*/
		if (other.tag == "SwitchOther") {
			//Debug.Log("On Calling Enter");
			Vector3 tempPos = other.transform.position;
			//other.transform.position = new Vector3(tempPos.x, tempPos.y - 0.4f, tempPos.z);
			other.transform.DOMove(new Vector3(tempPos.x, tempPos.y - 0.3f, tempPos.z), moveDuration);
			EventManager.TriggerEvent ("SwitchOn");
		}
	}

	void OnTriggerExit(Collider other){
		/*if (other.tag == "Calling") {
			//Debug.Log("On Calling Enter");
			Vector3 tempPos = other.transform.position;
			//other.transform.position = new Vector3(tempPos.x, tempPos.y - 0.4f, tempPos.z);
			other.transform.DOMove(new Vector3(tempPos.x, tempPos.y + 0.3f, tempPos.z), moveDuration);
			windyAgent.SetDestination(tempPos);
		}*/
		/*if (other.tag == "SwitchOther") {
			//Debug.Log("On Calling Enter");
			Vector3 tempPos = other.transform.position;
			//other.transform.position = new Vector3(tempPos.x, tempPos.y - 0.4f, tempPos.z);
			other.transform.DOMove(new Vector3(tempPos.x, tempPos.y + 0.3f, tempPos.z), moveDuration);

		}*/
	}


	void OnCalling () {
		if (Input.GetKeyDown(KeyCode.Space)){

			windyAgent.SetDestination (this.transform.position);
		}
	}

}
