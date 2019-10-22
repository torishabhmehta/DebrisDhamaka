using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    float hitforce = 1000f;
    public float damage = 100f;
    public Transform tip1;
    public Transform tip2;
    public ParticleSystem Flash;
    public attributeDefiner attribute;
    public GameObject Craft;
    public updateMethods updateMethod;
    
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        attribute = Craft.GetComponent<attributeDefiner>();
        updateMethod = Craft.GetComponent<updateMethods>();  
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("firelaser");
            StartCoroutine("firelaser");
        }
    }
    IEnumerator firelaser()
    {

        
        line.enabled = true;
        while(Input.GetButton("Fire1") && attribute.IsGame && !attribute.IsLoading)
        {   
            Flash.Play();
            Renderer r = line.GetComponent<Renderer>();
            r.material.mainTextureOffset = new Vector2(0, Time.time);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            line.SetPosition(0, ray.origin);

            updateMethod.removeFuel(0.1f);
           
            if (Physics.Raycast(ray, out hit, 64000))
            {   
                line.SetPosition(1, hit.point);
                Debug.Log(hit.transform.name);
                
                Target target = hit.transform.GetComponent<Target>();
                if(target!= null)
                {   
                    updateMethod.creditMoney(10);
                    target.TakeDamage(damage);
                }


                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(transform.forward*hitforce, hit.point);
                }
            }
            else
            {
                line.SetPosition(1, transform.forward * 1000);
            }
            yield return null;
        }
        line.enabled = false;
    }
}
