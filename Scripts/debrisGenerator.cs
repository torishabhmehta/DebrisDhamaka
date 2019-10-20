using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeptomoby.OrbitTools;
using System.IO;
using System;
using Random=UnityEngine.Random;
using Newtonsoft.Json;

[Serializable]
public class Debris
{
    public string OBJECT_ID;
    public string TLE_LINE1;
    public string TLE_LINE2;
    public string OBJECT_TYPE;
}

public class debrisGenerator : MonoBehaviour
{   
    public GameObject bolt;
    public GameObject nut;
    public GameObject sheet;
    public List<Debris> debrisData;

    public void LoadJson()
    {   
        string filename = @"C:\Users\Rishabh Mehta\New Unity Project\Assets\TLE.json";
        using (StreamReader r = new StreamReader(filename))
        {
            string json = r.ReadToEnd();
            debrisData = JsonConvert.DeserializeObject<List<Debris>>(json);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {   
        LoadJson();
        foreach (var item in debrisData)
        {
            if(item.OBJECT_TYPE == "DEBRIS") {
                Tle tle = new Tle(item.OBJECT_ID, item.TLE_LINE1, item.TLE_LINE2);

                const int Step = 360;
                Satellite sat = new Satellite(tle);
                List<Eci> coords = new List<Eci>();

                Eci eci = sat.PositionEci(900);
                Vector3 pos = new Vector3 ((float) eci.Position.X/64f,(float) eci.Position.Y/64f,(float) eci.Position.Z/64f);
                float decider = Random.value;

                if (decider  <= 0.33) {
                DebrisGenerator(bolt, pos);
                } else if (decider  > 0.33 && decider <= 0.66) {
                DebrisGenerator(nut, pos);
                } else {
                DebrisGenerator(sheet, pos);
                }
            }
        }
    }

    private void DebrisGenerator(GameObject Obj, Vector3 pos)
    {
        float rotX = (float) Random.Range(0.0F, 180.0F);
        float rotY = (float) Random.Range(0.0F, 180.0F);
        float rotZ = (float) Random.Range(0.0F, 180.0F);

        Vector3 rotation  = new Vector3(rotX, rotY, rotZ);
        var newDebris = Instantiate(Obj, pos, Quaternion.Euler(rotation));
        newDebris.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
