using UnityEngine;
using System.Collections;

public class CreateTargets : MonoBehaviour {
	public Rigidbody target;
	public int numTargets;

	// Use this for initialization
	void Start () {
		Vector3 p = gameObject.transform.position;
		for (int i=0; i<numTargets; i++) {
			float x0 = -10.0f + 20.0f * Random.value;
			float y0 = 10.0f * Random.value;
			float z0 = 3.0f + 2.0f*(float)i;
			Instantiate (target, new Vector3 (p.x+x0, p.y+y0, p.z+z0), Quaternion.identity);
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
