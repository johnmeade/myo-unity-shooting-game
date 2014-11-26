using UnityEngine;
using System.Collections;

public class RailsWithTargets : MonoBehaviour {
	public float xDir;
	public float zDir;
	public GameObject floorTile;
	public float floorTileSize;
	public GameObject target;
	public float targetFreq;

	private Vector3 dir;
	private Vector3 normDir;
	private float nextFloor; // count-down value to add another floor tile
	private Vector3 nextFloorPosn; // next floor tile position
	private Vector3 up = new Vector3 (0.0f, 1.0f, 0.0f);
	private Vector3 deltaTile;

	// Use this for initialization
	void Start () {
		dir = new Vector3 (xDir, 0.0f, zDir);
		normDir = dir.normalized;
		nextFloor = floorTileSize;
		nextFloorPosn = transform.position;
		// create starting tiles
		deltaTile = floorTileSize * normDir;
		for (int i=0; i<10; i++) {
			Instantiate (floorTile, nextFloorPosn - up, Quaternion.identity);
			nextFloorPosn += deltaTile; // update
		}
	}

	void addTarget(){
		Vector3 posn =  transform.position +
			           (10.0f + Mathf.Round (Random.value * 10.0f)) * dir + // in front of track
					    Mathf.Round((Random.value * 2.0f - 1.0f) * 6.0f) * new Vector3(-zDir, 0.0f, xDir) + // perpendicular from track
						Mathf.Round(Random.value * 10.0f) * new Vector3(0.0f, 1.0f, 0.0f); // random height above track
		Instantiate (target, posn, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		// Add targets
		if (Random.value < targetFreq) {
			addTarget();
		};
		// Move player
		Vector3 deltaDir = Time.deltaTime * dir;
		transform.position += deltaDir;
		// Add flooring
		nextFloor -= deltaDir.magnitude;
		if (nextFloor <= 0.0f) {
			// add another floor tile
			Instantiate (floorTile, nextFloorPosn - up, Quaternion.identity);
			// update vars
			nextFloor = floorTileSize;
			nextFloorPosn += deltaTile;
		};
	}
}
