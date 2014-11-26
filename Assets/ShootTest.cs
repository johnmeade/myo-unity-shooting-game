using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour {
	// BALL
	public Rigidbody myBall;
	public int power; // Newtons
	public int maxPower;

	void Start () {
		power = 1000;
		maxPower = 20000;
	}
	
	void OnGUI() {
		GUI.Box (new Rect (10, Screen.height - 30, Screen.width - 20, 20), ""); // BG
		GUI.Box (new Rect (10, Screen.height - 30, (Screen.width - 20) * ((float)power / (float)maxPower), 20), ""); // Fill
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			power += 200;
			power = power % maxPower;
		}
		else if (Input.GetMouseButtonUp(0)) {
			Rigidbody ball = Instantiate(myBall, Camera.main.transform.position, Quaternion.identity) as Rigidbody;
			ball.mass = 5;
			ball.AddForce( power * Camera.main.transform.forward );
			Destroy(ball, 10.0f);
			// TODO smarter destroy functionality
			power = 1000;
		}
	}
}
