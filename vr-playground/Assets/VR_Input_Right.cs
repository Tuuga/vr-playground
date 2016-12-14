using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_Input_Right : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	void Update() {
		
	}
}
