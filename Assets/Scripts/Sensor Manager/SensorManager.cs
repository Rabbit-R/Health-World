using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public static double tokyoLatitude;
    public static double tokyoLongitude;
    public static int stepsCount;
    public static int jumpCount;
    public static float sleepTimer;

    public static int stepsDemo; // (Demo) Steps = Step Counter * 100
    public static int indoorDemo; // (Demo) Indoor = Indoor Activities * 100
    public static float sleepDemo; // (Demo) Sleep = Sleep Timer * 100

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TokyoLatitude Information
        tokyoLatitude = GPS.tokyoLatitude;
        // TokyoLongitude Information
        tokyoLongitude = GPS.tokyoLongitude;
        // steps counter
        stepsCount = Pedometer.steps;
        // jumps counter
        jumpCount = Pedometer.jumpCount;
        // sleep time counter
        //if the time is after 22.pm and before 10.am, 
        //and the mobile phone is static for more than 30 mins, 
        //the output is !0;
        sleepTimer = Pedometer.sleepTimer;
        
        /*
        Health Energy System
        (Demo) Steps = Step Counter * 100
        (Demo) Indoor = Indoor Activities * 100
        (Demo) Sleep = Sleep Timer * 100
        */
        stepsDemo = stepsCount * 100;
        indoorDemo = jumpCount * 100;
        sleepDemo = sleepTimer * 100 / 3600; //转化为小时

        //Debug.Log("Steps Counter" + stepsCount); //Debug Information
    }
}
