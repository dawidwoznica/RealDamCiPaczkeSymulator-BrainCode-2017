using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller {  get { return SteamVR_Controller.Input((int) trackedObject.index); } }
    private SteamVR_TrackedObject trackedObject;

   
    void Start ()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	
	void Update ()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initializerd");
            return;
        }
        if (controller.GetPressDown(gripButton))
        {
            Debug.Log("Grip Button was just pressed");
        }
        if(controller.GetPressUp(gripButton))
        {
            Debug.Log("Grup button was just unpressed");
        }
     
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
    }
}
