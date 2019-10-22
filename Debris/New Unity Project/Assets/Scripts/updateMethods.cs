using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class updateMethods : MonoBehaviour
{
    public GameObject spaceCraft;
    public attributeDefiner attribute;

    void Start()
    {   
        spaceCraft = GameObject.Find("SpaceCraft");
        attribute = spaceCraft.GetComponent<attributeDefiner>();
    }

    public void Heat(float heat) 
    {
        float overheat = attribute.overheat;
        if (overheat > -1*heat) {
            overheat += heat;
        } else {
            overheat = 0f;
        }
        
        attribute.overheat = overheat;
    }

    public void addFuel (float change) {

        float fuel = attribute.fuel;

        if (fuel  < 100f-change) {
            fuel += change;
        } else {
            fuel = 100f;
        }

        attribute.fuel = fuel;
    }

    public void removeFuel (float change) {
        float fuel = attribute.fuel;

        if (fuel < change) {
            fuel = 0f;
        } else {
        fuel -= change;
        }

        attribute.fuel = fuel;
    }

    public void repair(int rep) 
    {
        int health = attribute.health;

        if (health  < 100-rep) {
            health += rep;
        } else {
            health = 100;
        }

        attribute.health = health;
    }

    public void damage (int dam) {

        int health = attribute.health;

        if (health < dam) {
            health = 0;
        } else {
        health -= dam;
        }

        attribute.health = health;
    }

    public void creditMoney(int credit) 
    {
        int money = attribute.money; 
        
        if (money + credit  < 0) {
            money = 0;
        } else {
            money += credit;
        }
        attribute.money = money;
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

}
