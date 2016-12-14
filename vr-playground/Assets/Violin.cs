using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violin : MonoBehaviour {

	public float frequency = 440;
	public float gain = 0.05f;

	float increment;
	float phase;
	float sampling_frequency = 48000;

	public Color blank, active;
	public MeshRenderer[] strings;

	public VR_Input_Left _lc;
	public VR_Input_Right _rc;

	bool _colorSet;

	void Update() {

		float[] spectrum = new float[256];

		AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

		for (int i = 1; i < spectrum.Length - 1; i++) {
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
		}
		
		if (_lc.IsTouchingPad()) {
			AudioListener.volume = 1f;
			
			frequency = _lc.OnRange(1000f);

			foreach (MeshRenderer mr in strings) {
				_colorSet = false;
				if (mr == strings[_lc.OnString()]) {
					mr.material.color = active;
				} else {
					mr.material.color = blank;
				}
			}
		} else if (!_colorSet) {
			foreach (MeshRenderer mr in strings) {
				mr.material.color = blank;
			}
			_colorSet = true;
			AudioListener.volume = 0f;
		}
	}

	void OnAudioFilterRead(float[] data, int channels) {
		// update increment in case frequency has changed
		increment = frequency * 2 * Mathf.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels) {
			phase = phase + increment;
			// this is where we copy audio data to make them “available” to Unity
			data[i] = (float)(gain * Mathf.Sin(phase));
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2)
				data[i + 1] = data[i];
			if (phase > 2 * Mathf.PI)
				phase = 0;
		}
	}
}
