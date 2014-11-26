using UnityEngine;
using System.Collections;

public class AddMusicLayerOnCollision : MonoBehaviour {
	public string colliderName;
	public string audioSourceObject;
	public string addMusicLayerFunction;

	private bool hasCollided = false;

	void OnCollisionEnter(Collision c){
		if (!hasCollided && c.gameObject.name == colliderName) {
			hasCollided = true;
			GameObject audioSource = GameObject.Find (audioSourceObject);
			audioSource.SendMessage (addMusicLayerFunction);
		}
	}
}
