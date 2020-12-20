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

    private static float volume;
    private float interval;
    private  int noise = 0;
    private int Timer = 0;
    private const float maxVolume = 30;//录音开启音量值

    public static int noiseCount = 0;
   

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
        sleepDemo = sleepTimer * 100 / 3600; //转化为小时\


        //volume = AudioData.volume;
        //noiseCount = AudioData.noiseCount;

        if (interval >= 1 ) // updatea per second
        {
            volume = AudioData.volume;
            //string v = volume.ToString();
            //statusText.text = v;
            Timer += 1; // Plus 1 second
            if(volume > maxVolume)
            {
                noise += 1;
            }


            interval = 0;
        }
        
        interval += Time.deltaTime;

        if(Timer == 60) // check result every 60 seconds
        {
            if(noise > 50)
            {
                //noiseFlag = true;
                noiseCount += 1;
            }

            Timer = 0;
            noise = 0;
            //Debug.Log(noiseCount);

        }

       
        
        

        //Debug.Log("Steps Counter" + stepsCount); //Debug Information
    }
}
