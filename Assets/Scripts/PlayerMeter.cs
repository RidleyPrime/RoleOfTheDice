using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMeter : MonoBehaviour
{
    public int meter; //the number that is our actual, current meter
    public int maxMeter =100; //our maximum meter, including overcharge
    public int overChargeLimit = 70; //point at which we can roll
    public Slider meterSlider; //the slider that represents our health

    private void Awake()
    {
        meterSlider = GetComponent<Slider>();
        meterSlider.maxValue = maxMeter;
        meterSlider.minValue = 0;
        meterSlider.SetDirection(Slider.Direction.LeftToRight, true); //I have no idea if this should be true or false
    }
    // Update is called once per frame
    void Update()
    {
        //UI Mumbo Jumbo, checks meter in real time. 
    }
   
    //Meter management, need an empty, a refill, and a recharge. Going to need to think of how I want the recharge to work
}