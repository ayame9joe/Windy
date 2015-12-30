using UnityEngine;
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

	public float minDis = 1;

	public GameObject windy;
	NavMeshAgent windyAgent;

	int callingTimes;

	int boxTappingTimes;

	float energyMoveDuration = 2;

	float moveDuration = 2;

	int maxGridNum = 15;
	// Use this for initialization
	void Start () {
		m_agent = this.GetComponent<NavMeshAgent> ();
		m_layerMask = 1 << 8;
		itemLayerMask = 1 << 9;
		windyAgent = windy.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
		TraverseOffMeshLink ();
		OnSwitchTap ();
		OnRotatedEnemyTap ();
		OnCallingTap ();
		OnDangerAlertTap ();
		OnBoxTap ();
		OnEnergyTap ();


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
								Vector3 tempVec3 = new Vector3(i * 2, j * 2 + transform.position.y * 0.5f, k * 2 + 1);
								if (Vector3.Distance(m_hitInfo.point, tempVec3) < 1f){
									desPos = tempVec3;
									this.transform.LookAt(tempVec3);
									m_agent.SetDestination(tempVec3);
									//Debug.Log (Vector3.Distance(m_hitInfo.point, tempVec3) );
								}
							}
						}
					}
					//desPos = new Vector3 (m_hitInfo.point.x, m_hitInfo.point.y /*+ transform.position.y * 0.5f */, m_hitInfo.point.z);

					//Debug.Log (m_hitInfo.point);

				}
			}
		}




	}

	void TraverseOffMeshLink () {
		if (m_agent.isOnOffMeshLink) {
			linkData = m_agent.currentOffMeshLinkData;
			this.transform.DOMove(linkData.endPos, moveDuration);
		}
		if (Vector3.Distance(this.transform.position, linkData.endPos) < 1){
			m_agent.Resume();
			m_agent.CompleteOffMeshLink();
		}
	}

	void OnSwitchTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Switch");
		for(int i = 0; i < gos.Length; i++) {
			if(Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray,out m_hitInfo, m_rayDistance, itemLayerMask))
				{
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
						if (m_hitInfo.transform == gos[i].transform) {
							gos[i].GetComponent<SwitchScript>().hasCheckedType = false;
						}
					}
				}
			}


			if (!gos[i].GetComponent<SwitchScript>().hasCheckedType) {
				//Debug.Log ("Switch On");
				gos[i].GetComponent<SwitchScript>().SwitchOn();
				gos[i].GetComponent<SwitchScript>().hasCheckedType = true;
			}
			else {
				gos[i].GetComponent<SwitchScript>().CheckType();
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

	void OnCallingTap () {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Calling");
		for (int i = 0; i < gos.Length; i++) {
			if (Input.GetMouseButtonDown(0)){
				if(Physics.Raycast(m_ray, out m_hitInfo, m_rayDistance)){
					if(Vector3.Distance(this.transform.position, gos[i].transform.position) < minDis)
					{
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
	}

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


}
