using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

	public float dx;
	public float dy;
	public float dz;
	public float dt;

	private float ix;
	private float iy;
	private float iz;

	void Start () {
		ix = transform.position.x;
		iy = transform.position.y;
		iz = transform.position.z;
		if (dt != 0) Destroy(gameObject, dt);
	}

	void Update () {
		bool flag = false;
		// TODO change to epsilon comparison for float safety
		if (dx != 0 && dx < 0 && transform.position.x < ix + dx) flag = true;
		if (dx != 0 && dx > 0 && transform.position.x > ix + dx) flag = true;

		if (dy != 0 && dy < 0 && transform.position.y < iy + dy) flag = true;
		if (dy != 0 && dy > 0 && transform.position.y > iy + dy) flag = true;

		if (dz != 0 && dz < 0 && transform.position.z < iz + dz) flag = true;
		if (dz != 0 && dz > 0 && transform.position.z > iz + dz) flag = true;

		if (flag) {
			Destroy(gameObject);
		};
	}
}
