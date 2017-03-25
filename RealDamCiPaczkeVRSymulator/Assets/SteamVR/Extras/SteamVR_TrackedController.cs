//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using Valve.VR;

public struct ClickedEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float padX, padY;
}

public delegate void ClickedEventHandler(object sender, ClickedEventArgs e);

public class SteamVR_TrackedController : MonoBehaviour
{
    public uint controllerIndex;
    public VRControllerState_t controllerState;
    public bool triggerPressed = false;
    public bool steamPressed = false;
    public bool menuPressed = false;
    public bool padPressed = false;
    public bool padTouched = false;
    public bool gripped = false;

    public event ClickedEventHandler MenuButtonClicked;
    public event ClickedEventHandler MenuButtonUnclicked;
    public event ClickedEventHandler TriggerClicked;
    public event ClickedEventHandler TriggerUnclicked;
    public event ClickedEventHandler SteamClicked;
    public event ClickedEventHandler PadClicked;
    public event ClickedEventHandler PadUnclicked;
    public event ClickedEventHandler PadTouched;
    public event ClickedEventHandler PadUntouched;
    public event ClickedEventHandler Gripped;
    public event ClickedEventHandler Ungripped;
    public GameObject Customer;

    public Transform[] SpawnPoints;
    // Use this for initialization
    void Start()
    {
        if (this.GetComponent<SteamVR_TrackedObject>() == null)
        {
            gameObject.AddComponent<SteamVR_TrackedObject>();
        }

		if (controllerIndex != 0)
		{
			this.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
			if (this.GetComponent<SteamVR_RenderModel>() != null)
			{
				this.GetComponent<SteamVR_RenderModel>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
			}
		}
		else
		{
			controllerIndex = (uint) this.GetComponent<SteamVR_TrackedObject>().index;
        }
    }

	public void SetDeviceIndex(int index)
	{
			this.controllerIndex = (uint) index;
	}

    public virtual void OnMenuClicked(ClickedEventArgs e)
    {
        if (MenuButtonClicked != null)
            MenuButtonClicked(this, e);
    }

    public virtual void OnMenuUnclicked(ClickedEventArgs e)
    {
        if (MenuButtonUnclicked != null)
            MenuButtonUnclicked(this, e);
    }

    // Update is called once per frame
    void Update()
    {
		var system = OpenVR.System;
		if (system != null && system.GetControllerState(controllerIndex, ref controllerState, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(VRControllerState_t))))
		{
            ulong menu = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_ApplicationMenu));
            if (menu > 0L && !menuPressed)
            {
                menuPressed = true;
                ClickedEventArgs e;
                e.controllerIndex = controllerIndex;
                e.flags = (uint)controllerState.ulButtonPressed;
                e.padX = controllerState.rAxis0.x;
                e.padY = controllerState.rAxis0.y;
                OnMenuClicked(e);
            }
            else if (menu == 0L && menuPressed)
            {
                menuPressed = false;
                ClickedEventArgs e;
                e.controllerIndex = controllerIndex;
                e.flags = (uint)controllerState.ulButtonPressed;
                e.padX = controllerState.rAxis0.x;
                e.padY = controllerState.rAxis0.y;
                OnMenuUnclicked(e);
                Spawn();
            }
        }
    }

    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

        Instantiate(Customer, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
    }
}
