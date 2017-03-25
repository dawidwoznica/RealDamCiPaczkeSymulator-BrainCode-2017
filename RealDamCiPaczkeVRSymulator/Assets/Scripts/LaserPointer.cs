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

    public Transform CameraRigTransform;
    
    public GameObject TeleportReticlePrefab;
   
    private GameObject _reticle;
   
    private Transform _teleportReticleTransform;
  
    public Transform HeadTransform;
  
    public Vector3 TeleportReticleOffset;
   
    public LayerMask TeleportMask;
   
    private bool _shouldTeleport;



    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        _laser = Instantiate(LaserPrefab);
        _laserTransform = _laser.transform;

        _reticle = Instantiate(TeleportReticlePrefab);
       
        _teleportReticleTransform = _reticle.transform;
    }
    
    void Update () {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;


            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, TeleportMask))
            {
                _hitPoint = hit.point;
                ShowLaser(hit);
                _reticle.SetActive(true);
                
                _teleportReticleTransform.position = _hitPoint + TeleportReticleOffset;
               
                _shouldTeleport = true;
            }
        }
        else
        {
            _laser.SetActive(false);
            _reticle.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && _shouldTeleport)
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
       
        _shouldTeleport = false;
       
        _reticle.SetActive(false);
       
        Vector3 difference = CameraRigTransform.position - HeadTransform.position;
       
        difference.y = 0;
        
        CameraRigTransform.position = _hitPoint + difference;
    }
}
