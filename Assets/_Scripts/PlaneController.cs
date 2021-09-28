using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    
    // Inspector Visible Members
    [SerializeField] private float _planeXSpeed             = 0.0f;
    [SerializeField] private float _planeZSpeed             = 0.0f;
    //[SerializeField] private float _positionPitchFactor     = -2.0f;
    [SerializeField] private float _controlPitchFactor      = -10.0f;
    //[SerializeField] private float _controlYawFactor        = -10.0f;
    [SerializeField] private float _controlRollFactor       = -10.0f;

    [SerializeField] private MeshGenerator _meshGenerator   = null;


    // Private Members
    private float _xMovement            = 0.0f;
    private float _yMovement            = 0.0f;
    private float _zMovement            = 0.0f;
    private float pitch                 = 0.0f;
    private float yaw                   = 0.0f;
    private float roll                  = 0.0f;

    private void Start()
    {
        _zMovement = transform.position.z;
    }

    void Update()
    {
        _xMovement = Input.GetAxis("Horizontal");
        _yMovement = Input.GetAxis("Vertical");

        MovePlane();  
        PlaneRotationController();
    }

    private void PlaneRotationController()
    {
        //float pitchDueToPosition = transform.position.y * _positionPitchFactor;
        float pitchDueToControlThrow = _yMovement * _controlPitchFactor;

        pitch   = /*pitchDueToPosition +*/ pitchDueToControlThrow;
        //yaw = transform.localPosition.x * _controlYawFactor;
        roll    = _xMovement * _controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void MovePlane()
    {
        float xPosition = transform.localPosition.x + _xMovement * _planeXSpeed * Time.deltaTime;
        float yPosition = transform.localPosition.y + _yMovement * _planeZSpeed * Time.deltaTime;
        float zPosition = transform.localPosition.z + _planeZSpeed * Time.deltaTime;
        transform.localPosition = new Vector3(xPosition, yPosition, zPosition);

        if(zPosition - _zMovement > 1)
        {
            int temp = (int)zPosition - (int)_zMovement;
            _meshGenerator.GenerateNewMesh((int)_xMovement, temp);
            _zMovement = zPosition;
        }
    }

}
