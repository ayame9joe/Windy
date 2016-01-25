using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovableScript : MonoBehaviour {

	public GameObject Movable;
	MovableController movableController;

	Vector3 m_Pos;

	public float duration = 1;

	public bool hasCheckedType = true;

	public float moveDis = 2;

	// Use this for initialization
	void Start () {
		movableController = Movable.GetComponent<MovableController> ();
		m_Pos = Movable.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void SwitchOn () {
		switch (movableController.movableType) {
		case MovableController.MovableType.left:
			Movable.transform.DOMove(new Vector3(m_Pos.x + moveDis, m_Pos.y, m_Pos.z), duration);
			break;
		case MovableController.MovableType.right:
			Movable.transform.DOMove(new Vector3(m_Pos.x - moveDis, m_Pos.y, m_Pos.z), duration);
			break;
		case MovableController.MovableType.up:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y + moveDis, m_Pos.z), duration);
			break;
		case MovableController.MovableType.down:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y - moveDis, m_Pos.z), duration);
			break;
		case MovableController.MovableType.forward:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z - moveDis), duration);
			break;
		case MovableController.MovableType.back:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z + moveDis), duration);
			break;
		}		 
	}

	public void CheckType () {
		if (Movable.transform.position == new Vector3 (m_Pos.x + moveDis, m_Pos.y, m_Pos.z)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		} else if (Movable.transform.position == new Vector3 (m_Pos.x - moveDis, m_Pos.y, m_Pos.z)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y + moveDis, m_Pos.z)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y - moveDis, m_Pos.z)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y, m_Pos.z + moveDis)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y, m_Pos.z - moveDis)) {
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z), duration);
		}

	}
}
