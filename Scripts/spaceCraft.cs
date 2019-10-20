using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spaceCraft : MonoBehaviour
{
    
    public Rigidbody rbd;
    public GameObject spaceCraftObject;
    public GameObject Craft;
    public Transform CraftTransform;
    public Rigidbody rbde;
    public Transform spaceCraftTransform;
    public float thrust;
    public float turn;
    public attributeDefiner attribute;

    public float speedH = 0.2f;
    public float speedV = 0.2f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {   
        attribute = Craft.GetComponent<attributeDefiner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        thrust = 0.003f;

        Vector3 thrustDir = Vector3.Normalize(Vector3.Cross(CraftTransform.position,CraftTransform.forward));

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (pitch > 45f) {
            pitch = 45f;
        } else if (pitch < -45f) {
            pitch = -45f;
        }


        CraftTransform.localEulerAngles = new Vector3(pitch, yaw, 0f);

        if (attribute.IsGame) {

           
            if (Input.GetKey(KeyCode.W)){
                rbd.AddTorque(thrustDir*thrust);
            }
            if (Input.GetKey(KeyCode.S)){
                rbd.AddTorque(thrustDir*-1*thrust);
            }
            
            
        }
        
    }
}