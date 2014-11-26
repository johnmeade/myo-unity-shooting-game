using UnityEngine;
using System.Collections;

public class PlayLayeredAudio : MonoBehaviour {
	public AudioSource audsrc;
	public AudioClip[] clips;

	private int numLayers;
	private int activeLayers = 0;
	private float nextTime;
	private int partialClip = 0;
	private float secsPerClip;
	private int layersCurrentlyPlaying = 0;

	void Awake () {
		// find clips
		numLayers = clips.GetLength (0);
		secsPerClip = clips[0].length;
	}
	
	void removeAudioLayer () {
		if (activeLayers > 0) {
			activeLayers--;
		}
	}

	void addMusicLayer () {
		if ( activeLayers < numLayers ) {
			activeLayers++;
		}
		if ( activeLayers > layersCurrentlyPlaying ) {
			partialClip = activeLayers - layersCurrentlyPlaying;
		}
	}

	private IEnumerator IAddMusicLayer (AudioClip cl){
		// Play a clip from some percentage into it
		// do this by getting raw audio data and splicing it
		// spin enumerator until we can get info from clip
		while (!cl.isReadyToPlay) {
			yield return null;
		}
		// percent := length from current time to nextTime
		float percent = (nextTime - Time.time) / secsPerClip;
		int sam = cl.samples;
		int sampLeft = (int)(sam * percent); // samples left between now and nextTime
		int ch = cl.channels;
		int fr = cl.frequency;
		float[] dat = new float[sampLeft * ch]; // holds cl data
		cl.GetData(dat, sam - sampLeft);
		AudioClip ac = AudioClip.Create ("temp", sampLeft, ch, fr, false, false);
		ac.SetData( dat, 0 ); // create partial clip
		// spin the enumerator until ready
		while (!ac.isReadyToPlay) {
			yield return null;
		}
		audsrc.PlayOneShot (ac); // play
	}

	void Update() {
		// play partials
		for (int i=0; i < partialClip; i++) {
			AudioClip cl = clips[layersCurrentlyPlaying + i];
			StartCoroutine(IAddMusicLayer(cl));
		}
		partialClip = 0;
		// audio step
		if (Time.time > nextTime){
			for (int i = 0; i < activeLayers; i++) {
				audsrc.PlayOneShot((AudioClip)clips[i]);
			}
			nextTime = Time.time + secsPerClip;
			layersCurrentlyPlaying = activeLayers;
		}
	}
}
