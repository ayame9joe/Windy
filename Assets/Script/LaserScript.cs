using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LaserScript : MonoBehaviour {

	
	public enum LaserType {
		left,
		right,
		forward,
		back
	};
	
	public LaserType laserType;

	Vector3 curPos;

	// Use this for initialization
	void Start () {
		curPos = this.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		LaserDestroy ();
	
	}

	void LaserDestroy () {
		switch (laserType) {
		case LaserType.left:
			this.transform.DOMove(new Vector3(5, curPos.y, curPos.z), 2);//.SetRelative();
			if (this.transform.position == new Vector3 (5, curPos.y, curPos.z)) {
				Destroy(this.gameObject);
			}
			break;
		case LaserType.right:
			this.transform.DOMove(new Vector3(-5, curPos.y, curPos.z), 2).SetRelative();
			if (this.transform.position == new Vector3 (-5, curPos.y, curPos.z)) {
				Destroy(this.gameObject);
			}
			break;
		case LaserType.forward:
			this.transform.DOMove(new Vector3(curPos.x, curPos.y, -5), 2).SetRelative();
			if (this.transform.position == new Vector3 (curPos.x, curPos.y, -5)) {
				Destroy(this.gameObject);
			}
			break;
		case LaserType.back:
			this.transform.DOMove(new Vector3(curPos.x, curPos.y, 5), 2).SetRelative();
			if (this.transform.position == new Vector3 (curPos.x, curPos.y, 5)) {
				Destroy(this.gameObject);
			}
			break;
		}
	}
}
