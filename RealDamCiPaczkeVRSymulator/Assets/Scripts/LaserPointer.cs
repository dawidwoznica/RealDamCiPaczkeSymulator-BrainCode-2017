using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject LaserPrefab;
    
    private GameObject _laser;
    
    private Transform _laserTransform;
    
    private Vector3 _hitPoint;

    public Transform cameraRigTransform;
    
    public GameObject teleportReticlePrefab;
   
    private GameObject reticle;
   
    private Transform teleportReticleTransform;
  
    public Transform headTransform;
  
    public Vector3 teleportReticleOffset;
   
    public LayerMask teleportMask;
   
    private bool shouldTeleport;


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        _laser = Instantiate(LaserPrefab);
        _laserTransform = _laser.transform;

        reticle = Instantiate(teleportReticlePrefab);
       
        teleportReticleTransform = reticle.transform;
    }
    
    void Update () {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;


            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                _hitPoint = hit.point;
                ShowLaser(hit);
                reticle.SetActive(true);
                
                teleportReticleTransform.position = _hitPoint + teleportReticleOffset;
               
                shouldTeleport = true;
            }
        }
        else
        {
            _laser.SetActive(false);
            reticle.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }
    }

    private void ShowLaser(RaycastHit hit)
    {      
        _laser.SetActive(true);      
        _laserTransform.position = Vector3.Lerp(trackedObj.transform.position, _hitPoint, .5f);       
        _laserTransform.LookAt(_hitPoint);   
        _laserTransform.localScale = new Vector3(_laserTransform.localScale.x, _laserTransform.localScale.y,
            hit.distance);
    }


    private void Teleport()
    {
       
        shouldTeleport = false;
       
        reticle.SetActive(false);
       
        Vector3 difference = cameraRigTransform.position - headTransform.position;
       
        difference.y = 0;
        
        cameraRigTransform.position = _hitPoint + difference;
    }
}
