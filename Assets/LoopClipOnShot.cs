using UnityEngine;
using System.Collections;

public class LoopClipOnShot : MonoBehaviour {
	
	public AudioSource audsrc;
	public AudioClip Lead1;
	public ArrayList startTimes = new ArrayList();
	public ArrayList clips = new ArrayList();
	public float secPerBeat = 1.0f/2.13333333f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			// add desired clip
			clips.Add (Lead1);
			// schedule sound to play
			float playNext = Time.time;
			startTimes.Add (playNext);
		};
		// play all clips
		for (int i = 0; i < startTimes.Count; i++) {
			float startTime = (float)startTimes[i];
			AudioClip clip = (AudioClip)clips[i];
			if (Time.time > startTime) {
				audsrc.PlayOneShot(clip);
				startTimes[i] = startTime + 4.0f*secPerBeat;
			};
		};
	}
}
