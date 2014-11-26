using UnityEngine;
using System.Collections;

public class ShootRigidBody : MonoBehaviour {
	// BALL
	public Rigidbody ballBullet;
	public int minPower; // Newtons
	public int maxPower;
	public int numPowerSteps;
	private int power;
	private int powerStep;

	void Start () {
		power = minPower;
		powerStep = (int)((maxPower - minPower) / (float)numPowerSteps);
	}

	void OnGUI() {
		GUI.Box (new Rect (10, Screen.height - 30, Screen.width - 20, 20), ""); // BG
		GUI.Box (new Rect (10, Screen.height - 30, (Screen.width - 20) * ((float)power / (float)maxPower), 20), ""); // Fill
		GUI.Box (new Rect (Screen.width/2, Screen.height/2, 1, 1), ""); // TODO better crosshairs
	}
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			power += powerStep;
			power = Mathf.Min(power, maxPower);
		}
		else if (Input.GetMouseButtonUp(0)) {
			Rigidbody ball = Instantiate(ballBullet, Camera.main.transform.position, Quaternion.identity) as Rigidbody;
			ball.mass = 5;
			ball.AddForce( power * Camera.main.transform.forward );
			power = minPower;
		}
	}
}
