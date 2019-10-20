using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{   
    public GameObject spaceCraft;
    public attributeDefiner attribute;
    public Image speedBar;
    public Text speedText;
    public Image overheatBar;
    public Text overheatText;
    public Image healthBar;
    public Text healthText;
    public Image fuelBar;
    public Text fuelText;
    public Text moneyText;
    

    void Start()
    {
        spaceCraft = GameObject.Find("SpaceCraft");
        attribute = spaceCraft.GetComponent<attributeDefiner>();
    }

    // Update is called once per frame
    void Update()
    {
        speedBar.fillAmount = attribute.speed/attribute.maxSpeed;
        speedText.text = "" + (int) attribute.speed + " Km/s";

        overheatBar.fillAmount = attribute.overheat/100f;
        if ((int) attribute.overheat > 0) {
            overheatText.text = "" +(int) attribute.overheat;
        }

        healthBar.fillAmount = attribute.health/100f;
        healthText.text = "" + attribute.health;

        fuelBar.fillAmount = attribute.fuel/100f;
        fuelText.text = "" + (int) attribute.fuel;

        moneyText.text = "$" + attribute.money;
    }
}
