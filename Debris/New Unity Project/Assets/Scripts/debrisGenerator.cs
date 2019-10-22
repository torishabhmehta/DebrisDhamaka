using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zeptomoby.OrbitTools;
using System.IO;
using System;
using Random = UnityEngine.Random;
using Newtonsoft.Json;
using System.Net;

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
    // public List<Debris> debrisData;
    public GameObject spaceCraft;
    public attributeDefiner attribute;
    public GameObject LoadingScreen;
    public Text Loading;
    public Image Progress;
    public Text ProgressText;

    IEnumerator LoadDebris() {
        spaceCraft = GameObject.Find("SpaceCraft");
        attribute = spaceCraft.GetComponent<attributeDefiner>();
        Loading.text = "Downloading Game Debris Data";
        yield return new WaitForSeconds(1); 
        string jsonURL = @"https://worldwind.arc.nasa.gov/spacebirds/data/TLE.json";
        WWW download = new WWW( jsonURL );
        while( !download.isDone ) {
            Progress.fillAmount = download.progress;
            float prog = download.progress*100;
            ProgressText.text = "" + (int) prog + "%";
            
            yield return null;
        }
        
        yield return download;
        List<Debris> debrisData = JsonConvert.DeserializeObject<List<Debris>>(download.text);

        Loading.text = "Loading Game Debris.";
        

        foreach (var item in debrisData)
        {
            if (item.OBJECT_TYPE == "DEBRIS")
            {
                Tle tle = new Tle(item.OBJECT_ID, item.TLE_LINE1, item.TLE_LINE2);
                Satellite sat = new Satellite(tle);
                List<Eci> coords = new List<Eci>();

                Eci eci = sat.PositionEci(900);
                
                Vector3 pos = new Vector3((float)eci.Position.X, (float)eci.Position.Y, (float)eci.Position.Z);
                Debug.Log(pos);
                float decider = Random.value;

                if (decider <= 0.33)
                {
                    DebrisGenerator(bolt, pos);
                }
                else if (decider > 0.33 && decider <= 0.66)
                {
                    DebrisGenerator(nut, pos);
                }
                else
                {
                    DebrisGenerator(sheet, pos);
                }
            }
        }
        yield return new WaitForSeconds(1); 
        LoadingScreen.SetActive(false);
        attribute.IsLoading = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(LoadDebris());

    }

    private void DebrisGenerator(GameObject Obj, Vector3 pos)
    {
        float rotX = (float)Random.Range(0.0F, 180.0F);
        float rotY = (float)Random.Range(0.0F, 180.0F);
        float rotZ = (float)Random.Range(0.0F, 180.0F);

        Vector3 rotation = new Vector3(rotX, rotY, rotZ);
        var newDebris = Instantiate(Obj, pos, Quaternion.Euler(rotation));
        newDebris.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
