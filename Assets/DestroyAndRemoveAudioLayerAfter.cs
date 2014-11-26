using UnityEngine;
using System.Collections;

public class DestroyAndRemoveAudioLayerAfter : MonoBehaviour {
	
	public string audioPlayerObjectName;
	public float dx;
	public float dy;
	public float dz;
	public float dt;
	public string[] collisionTargets;
	
	private float ix;
	private float iy;
	private float iz;
	private bool hasCollided = false;
	
	int findIndexStr(string[] arr, string elem) {
		for (var i=0; i<arr.GetLength(0); i++) {
			if (elem == arr[i]) return i;
		}
		return -1;
	}
	
	void Start () {
		ix = transform.position.x;
		iy = transform.position.y;
		iz = transform.position.z;
		if (dt != 0) Destroy(gameObject, dt);
	}
	
	void OnCollisionEnter(Collision c){
		if (!hasCollided && findIndexStr(collisionTargets, c.gameObject.name) != -1) {
			hasCollided = true;
		}
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
			if (!hasCollided) {
				GameObject audioSource = GameObject.Find (audioPlayerObjectName);
				audioSource.SendMessage("removeAudioLayer");
			}
		};
	}
}
