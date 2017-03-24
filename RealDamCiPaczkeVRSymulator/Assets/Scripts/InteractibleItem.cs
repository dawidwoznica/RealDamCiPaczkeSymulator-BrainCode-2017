using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour
{

    public Rigidbody Rigidbody;

    private bool _currentlyInteracting;

    private WandController _attachedWand;

    private Transform _interactionPoint;

    private Vector3 _posDelta;
    private Quaternion _rotationDelta;
    private float _angle;
    private Vector3 _axis;
    private float _rotationFactor = 400f;
    private float _velocityFactor = 20000f;


	void Start ()
	{
	    Rigidbody = GetComponent<Rigidbody>();
	    _interactionPoint = new GameObject().transform;
	    _velocityFactor /= Rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
	    if (_attachedWand && _currentlyInteracting)
	    {
	        _posDelta = _attachedWand.transform.position - _interactionPoint.position;
	        this.Rigidbody.velocity = _posDelta * _velocityFactor * Time.fixedDeltaTime;

	        _rotationDelta = _attachedWand.transform.rotation * Quaternion.Inverse(_interactionPoint.rotation);
            _rotationDelta.ToAngleAxis(out _angle, out _axis);

	        if (_angle > 180)
	        {
	            _angle -= 360;
	        }

	        this.Rigidbody.angularVelocity = (Time.fixedDeltaTime * _angle * _axis) * _rotationFactor;
	    }
	}


    public void BeginInteraction(WandController wand)
    {
        _attachedWand = wand;
        _interactionPoint.position = wand.transform.position;
        _interactionPoint.rotation = wand.transform.rotation;
        _interactionPoint.SetParent(transform, true);

        _currentlyInteracting = true;
    }

    public void EndInteraction(WandController wand)
    {
        if (wand == _attachedWand)
        {
            _attachedWand = null;
            _currentlyInteracting = false;
        }
    }

    public bool IsInteracting()
    {
        return _currentlyInteracting;
    }
}
