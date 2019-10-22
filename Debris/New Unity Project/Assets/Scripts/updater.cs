using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updater : MonoBehaviour
{
    public GameObject spaceCraft;
    public GameObject spaceCraftAnchor;
    public Rigidbody rbdAnchor;
    public attributeDefiner attribute;
    public updateMethods updateMethod;

    void Start()
    {   
        spaceCraft = GameObject.Find("SpaceCraft");
        spaceCraftAnchor = GameObject.Find("SpaceCraftAnchor");
        rbdAnchor = spaceCraftAnchor.GetComponent<Rigidbody>();
        attribute = spaceCraft.GetComponent<attributeDefiner>();
        updateMethod = spaceCraft.GetComponent<updateMethods>();
    }

    private void OnTriggerEnter(Collider other) {   
        updateMethod.damage(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector3 pos = spaceCraft.transform.localPosition;
        Vector3 r = spaceCraft.transform.position;
        Vector3 angVelocity = rbdAnchor.angularVelocity;
        Vector3 velocity = Vector3.Cross(r, angVelocity);

        float orbit = pos.magnitude*attribute.convFactor*1000f;
        float optimalSpeed  = 19977378.01f/Mathf.Sqrt(orbit);
        float thrust = Mathf.Abs(Mathf.Pow(attribute.speed*1000,2) - Mathf.Pow(optimalSpeed,2))/(orbit);

        if (attribute.overheat > 0f || attribute.speed > attribute.maxSpeed) {
            updateMethod.Heat((attribute.speed - attribute.maxSpeed)*attribute.overheatFactor);
        }

        if (attribute.overheat >= 100f) {
            updateMethod.damage((int) attribute.overheat- (int)100f);
        }

        updateMethod.removeFuel(thrust*attribute.fuelFactor);

        attribute.speed = velocity.magnitude*attribute.convFactor;
    }
}
