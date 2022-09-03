using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steerAngle;
    [SerializeField] private float Traction;

    public Transform lw, rw;

    private float dragAmount = 0.99f;
    
    private Vector3 moveVec;
    private Vector3 rotVec;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        moveVec += transform.forward * carSpeed * Time.deltaTime;
        transform.position += moveVec * Time.deltaTime;

        rotVec += new Vector3(0, Input.GetAxis("Horizontal"), 0);
        
        transform.Rotate(Vector3.up*Input.GetAxis("Horizontal")*steerAngle*Time.deltaTime*moveVec.magnitude);

        moveVec *= dragAmount;
        moveVec = Vector3.ClampMagnitude(moveVec, maxSpeed);
        moveVec = Vector3.Lerp(moveVec.normalized, transform.forward, Traction * Time.deltaTime) * moveVec.magnitude;

        rotVec = Vector3.ClampMagnitude(rotVec, steerAngle);
        
        lw.localRotation=Quaternion.Euler(rotVec);
        rw.localRotation=Quaternion.Euler(rotVec);
    }
}
