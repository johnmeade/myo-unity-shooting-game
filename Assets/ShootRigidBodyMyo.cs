using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class ShootRigidBodyMyo : MonoBehaviour {

	public GameObject myo;
	public Rigidbody ballBullet;
	public int minPower; // Newtons
	public int maxPower;
	public int numPowerSteps;

	private int power;
	private int powerStep;
	private Pose _lastPose = Pose.Unknown;
	
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
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		if (thalmicMyo.pose == Pose.Fist) {
			power += powerStep;
			power = Mathf.Min (power, maxPower);
		}
		
		if (_lastPose == Pose.Fist && thalmicMyo.pose == Pose.Rest) {
			Rigidbody ball = Instantiate (ballBullet, Camera.main.transform.position, Quaternion.identity) as Rigidbody;
			ball.mass = 5;
			ball.AddForce (power * Camera.main.transform.forward);
			power = minPower;
		}
		
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
		}
	}
}