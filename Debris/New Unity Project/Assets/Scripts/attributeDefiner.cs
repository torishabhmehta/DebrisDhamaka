using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attributeDefiner : MonoBehaviour
{
    public int health;
    public int money;
    public float fuel;
    public float overheat;
    public float speed;
    public bool IsGame;
    public float convFactor;
    public float maxSpeed;
    public float overheatFactor;
    public float fuelFactor;
    public bool IsLoading;

    public GameObject GameOver;
    public GameObject LoadingScreen;

    // Update is called once per frame
    void Awake() {
        GameOver = GameObject.Find("GameOver");
        GameOver.SetActive(false);
        LoadingScreen = GameObject.Find("Loading");
    }
    void Start()
    {   
        IsGame = true;
        health = 102;
        money = 0;
        fuel = 101f;
        overheat = 0f;
        speed  = 0f;
        convFactor = 1f;
        maxSpeed = 250f;
        overheatFactor = 0f;
        fuelFactor = 0f;
        IsLoading = true;
    }

    void Update()
    {   
        if (!IsLoading) {
            LoadingScreen.SetActive(false);
        }

        if (health <= 0 || fuel <= 0f) {
            Cursor.lockState = CursorLockMode.None;
            IsGame = false;
            GameOver.SetActive(true);
        }
    }
}
