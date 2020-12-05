using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdviseManager : MonoBehaviour
{
    public static int stepsST;//步数状态
    public static int indoorST;//室内运动(跳跃数)状态
    public static int sleepST;//睡眠时间状态
    public static int HEST;//健康能量状态

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //根据当前步数给出建议
        if (SensorManager.stepsCount <= 10) stepsST = 1;
        else if (SensorManager.stepsCount > 10 && SensorManager.stepsCount <= 20) stepsST = 2;
        else if (SensorManager.stepsCount > 20) stepsST = 3;
        switch (stepsST)
        {
            case 1: System.Console.WriteLine("Maybe you need to walk more to get more healthy energy.");break;
            case 2: System.Console.WriteLine("The appropriate number of steps has been converted into healthy energy. Come on!"); break;
            case 3: System.Console.WriteLine("A large number of steps have been converted into healthy energy. Good job!"); break;
        }

        //根据当前室内运动量给出建议
        if (SensorManager.jumpCount < 5) indoorST = 1;
        else if (SensorManager.jumpCount >= 5 && SensorManager.jumpCount < 10) indoorST = 2;
        else if (SensorManager.jumpCount >= 10) indoorST = 3;
        switch (indoorST)
        {
            case 1: System.Console.WriteLine("We don't get healthy energy from indoor exercise. Proper indoor exercise may be a good thing for you."); break;
            case 2: System.Console.WriteLine("We have recorded proper indoor exercise and have harvested a lot of healthy energy. Come on!"); break;
            case 3: System.Console.WriteLine("A huge amount of health energy has been accumulated due to a lot of indoor exercise. Well done!"); break;
        }

        //根据睡眠时间给出建议
        if (SensorManager.sleepTimer < 4) sleepST = 1;
        else if (SensorManager.sleepTimer >= 4 && SensorManager.sleepTimer < 6) sleepST = 2;
        else if (SensorManager.sleepTimer >= 6 ) sleepST = 3;
        switch (sleepST)
        {
            case 1: System.Console.WriteLine("If you don't take a good rest, you will lose energy all day. It's time to adjust your schedule!"); break;
            case 2: System.Console.WriteLine("Maybe you need longer sleep time to make your life healthier."); break;
            case 3: System.Console.WriteLine("You have got enough sleep, have a good day!"); break;
        }

        //健康能量达到一定等级后的显示消息
        HEST = HealthEnergy.levelHE;
        switch (HEST)
        {
            case 1: System.Console.WriteLine("Almost nothing happends to the world."); break;
            case 2: System.Console.WriteLine("Animals become more lively and interesting."); break;
            case 3: System.Console.WriteLine("More animals have come to the world."); break;
        }
    }
}
