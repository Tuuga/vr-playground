using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_Input_Left : MonoBehaviour {

	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	public bool IsTouchingPad () {
		return controller.GetTouch(touchPad);
	}

	public int OnString () {
		return Mathf.RoundToInt((controller.GetAxis(touchPad).x + 1) * 1.5f);
	}

	public float OnRange (float r) {
		return (controller.GetAxis(touchPad).x + 1) * (r / 2);
	}
}
