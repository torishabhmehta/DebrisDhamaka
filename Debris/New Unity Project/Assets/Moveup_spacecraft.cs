using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveup_spacecraft : MonoBehaviour
{
    
    public float movementSpeed = 5f;
    public Rigidbody spacecraft;
    void Start()
    {
        
    }

    
    void Update()
    {


        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.UpArrow))
        {
           transform.position = transform.position + new Vector3(0,movementSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            spacecraft.AddForce(0,-1000f * Time.deltaTime,0);
        }
    }
}
