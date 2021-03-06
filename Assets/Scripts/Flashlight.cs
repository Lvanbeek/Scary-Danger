﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    //Flashlight Management variables
    private bool inHands;
    private bool flashlightEnabled;
    public GameObject lightObj;
    public int energyPerBattery;
    public float maxEnergy;
    private float currentEnergy;

    //Battery interaction variables
    private int batteries;
    private float usedEnergy;
    
    // Use this for initialization
    void Start () {
        maxEnergy = energyPerBattery * batteries;
        currentEnergy = maxEnergy;
	}

	
	// Update is called 50 times per second
	void FixedUpdate () {
        maxEnergy = energyPerBattery * batteries;
        currentEnergy = maxEnergy;
        //Flashlight on if there is energy in it and it is in the players hands
        //Bug: Player does not need to be holding the flashlight only needs to be in the box collider
        if (flashlightEnabled)
        {
            lightObj.SetActive(true);
            if (currentEnergy <= 0)
            {
                lightObj.SetActive(false);
                batteries = 0;
            }

            if (currentEnergy > 0)
            {
                lightObj.SetActive(true);
                currentEnergy -= .5f * Time.deltaTime;
                usedEnergy += .5f * Time.deltaTime;
            }

            if (usedEnergy >= 50)
            {
                batteries -= 1;
                usedEnergy = 0;
            }
            // print("Batteries: " + batteries);
            // print("usedEnergy: " + usedEnergy);
        }
        else
        {
            lightObj.SetActive(false);
        }
    }

    //Charges Flashlight when battery collides with it
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Battery")
        {
            batteries += 1;
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //If the flashlight is in the players hands inHands = True
        if (other.tag == "Hands")
            inHands = true;
    }

    public void OnTriggerExit(Collider other)
    {
        inHands = false;
    }

    public void ToggleSwitch()
    {
        if (inHands)
        {
            flashlightEnabled = !flashlightEnabled;
            FixedUpdate();
        }
    }

    public bool IsOn()
    {
        if(flashlightEnabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
