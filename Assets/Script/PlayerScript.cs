using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Ray m_ray;
	RaycastHit m_hitInfo;
	public float m_rayDistance = 1000;

	LayerMask m_layerMask;

	Vector3 desPos;

	NavMeshAgent m_agent;
	// Use this for initialization
	void Start () {
		m_agent = this.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
	
	}

	void Move () {

		if (Input.GetMouseButtonDown (0)) {
			m_layerMask = 1 << 8;
			// 根据鼠标在屏幕空间的位置计算射线
			m_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			// 进行三维场景中的射线求交
			if (Physics.Raycast (m_ray, out m_hitInfo, m_rayDistance, m_layerMask)) {
				if (m_hitInfo.transform.tag == "Floor" || m_hitInfo.transform.tag == "WindThrough") {
					desPos = new Vector3 (m_hitInfo.point.x, this.transform.position.y, m_hitInfo.point.z);
					this.transform.LookAt(desPos);
					m_agent.destination = desPos;
				}
			}
		}


	}


}
