using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {

    private Valve.VR.EVRButtonId _gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId _triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device _controller {  get { return SteamVR_Controller.Input((int) _trackedObject.index); } }
    private SteamVR_TrackedObject _trackedObject;

    HashSet<InteractibleItem> objectsHoveringOver = new HashSet<InteractibleItem>();

    private InteractibleItem closestItem;
    private InteractibleItem interactingItem;

   
    void Start ()
    {
        _trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	
	void Update ()
    {
        if (_controller == null)
        {
            Debug.Log("Controller not initializerd");
            return;
        }

        if (_controller.GetPressDown(_gripButton));
        {
            float minDistance = float.MaxValue;

            float distance;
            foreach (InteractibleItem item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }
            interactingItem = closestItem;

            if (interactingItem)
            {
                if (interactingItem.IsInteracting())
                {
                    interactingItem.EndInteraction(this);
                }

                interactingItem.BeginInteraction(this);
            }

        }

        if(_controller.GetPressUp(_gripButton) && interactingItem != null)
        {
            interactingItem.EndInteraction(this);
        }
     
	}

    private void OnTriggerEnter(Collider other)
    {
        InteractibleItem collidedItem = other.GetComponent<InteractibleItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractibleItem collidedItem = other.GetComponent<InteractibleItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }
    }
}
