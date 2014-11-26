using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {
	
	public float tAxisX;
	public float tAxisZ;
	public float xBound;
	public float zBound;
	public float amplitude;
	public float frequency;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;
	private float reverse; // flag to reverse wave direction
	private ArrayList initialYValues = new ArrayList();

	// Use this for initialization
	void Start () {
		Vector3 parPosn = transform.position;
		minX = parPosn.x - xBound;
		minX = parPosn.x + xBound;
		minZ = parPosn.z - zBound;
		minZ = parPosn.z + zBound;
		foreach (Transform child in transform) {
			Vector3 posn = child.position;
			initialYValues.Add (posn.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 parPosn = transform.position;
		// check bounds
		if (parPosn.x < minX || parPosn.x > maxX || parPosn.z < minZ || parPosn.z > maxZ) {
			reverse = -1.0f;
		} else {
			reverse = 1.0f;
		}
		// update posn
		float dx = reverse * Time.deltaTime * tAxisX;
		float dy = amplitude * Mathf.Sin (frequency * Time.time);
		float dz = reverse * Time.deltaTime * tAxisZ;

		int n = 0;
		foreach (Transform child in transform) {
			Vector3 posn = child.position;
			float newX = posn.x + dx;
			float newY = (float)initialYValues[n] + dy;
			float newZ = posn.z + dz;
			child.position = new Vector3 (newX, newY, newZ);
			n++;
		}
	}
}
