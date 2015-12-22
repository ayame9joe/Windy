using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SwitchScript : MonoBehaviour {

	public GameObject Movable;
	MovableController movableController;

	Vector3 m_Pos;

	public float duration = 1;

	public bool hasCheckedType = true;

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
			Movable.transform.DOMove(new Vector3(m_Pos.x + 1, m_Pos.y, m_Pos.z), duration);
			break;
		case MovableController.MovableType.right:
			Movable.transform.DOMove(new Vector3(m_Pos.x - 1, m_Pos.y, m_Pos.z), duration);
			break;
		case MovableController.MovableType.up:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y + 1, m_Pos.z), duration);
			break;
		case MovableController.MovableType.down:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y - 1, m_Pos.z), duration);
			break;
		case MovableController.MovableType.forward:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z - 1), duration);
			break;
		case MovableController.MovableType.back:
			Movable.transform.DOMove(new Vector3(m_Pos.x, m_Pos.y, m_Pos.z + 1), duration);
			break;
		}		 
	}

	public void CheckType () {
		if (Movable.transform.position == new Vector3 (m_Pos.x + 1, m_Pos.y, m_Pos.z)) {
			movableController.movableType = MovableController.MovableType.right;
		} else if (Movable.transform.position == new Vector3 (m_Pos.x - 1, m_Pos.y, m_Pos.z)) {
			movableController.movableType = MovableController.MovableType.left;
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y + 1, m_Pos.z)) {
			movableController.movableType = MovableController.MovableType.down;
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y - 1, m_Pos.z)) {
			movableController.movableType = MovableController.MovableType.up;
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y, m_Pos.z + 1)) {
			movableController.movableType = MovableController.MovableType.forward;
		} else if (Movable.transform.position == new Vector3 (m_Pos.x, m_Pos.y, m_Pos.z - 1)) {
			movableController.movableType = MovableController.MovableType.back;
		}

	}
}
