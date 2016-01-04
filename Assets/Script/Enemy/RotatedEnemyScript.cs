using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotatedEnemyScript : MonoBehaviour {

	int rotateTimes;
	public enum RotateType {
		left,
		right
	}

	public float duration = 2;
	
	public RotateType rotateType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RotateOn () {
		if (rotateTimes < 4) {
			rotateTimes++;
		} else {
			rotateTimes = 0;
		}
		switch (rotateType) {
		case RotateType.left:
			this.transform.DORotate(new Vector3(0, rotateTimes * 90, 0), duration);
			break;
		case RotateType.right:
			this.transform.DORotate(new Vector3(0, - rotateTimes * 90, 0), duration);
			break;
		}
	}
}
