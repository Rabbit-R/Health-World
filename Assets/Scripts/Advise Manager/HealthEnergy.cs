using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnergy : MonoBehaviour
{
    public static int stepsHE;//步数转化为健康能量
    public static int indoorHE;//跳跃数转化为健康能量
    public static float sleepHE;//睡眠时间转化为健康能量

    public static int gender;//性别
    public static int age;//年龄

    public static double valueHE;//健康能量值
    public static int levelHE;//健康能量等级

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stepsHE = SensorManager.stepsCount * 100;
        indoorHE = SensorManager.jumpCount * 100;
        sleepHE = SensorManager.sleepTimer * 100;

        if (gender == 0)//男性
        {
            if (age < 18)
            {
                valueHE = stepsHE * 0.6 + indoorHE * 0.8 + sleepHE * 0.8;
            }
            else if (age >= 18 && age < 28)
            {
                valueHE = stepsHE * 0.7 + indoorHE * 0.9 + sleepHE * 0.8;
            }
            else if (age >= 28 && age < 35)
            {
                valueHE = stepsHE * 0.8 + indoorHE * 1.0 + sleepHE * 0.8;
            }
            else if (age >= 35 && age < 45)
            {
                valueHE = stepsHE * 0.9 + indoorHE * 1.1 + sleepHE * 0.9;
            }
            else if (age >= 45 && age < 55)
            {
                valueHE = stepsHE * 1.0 + indoorHE * 1.2 + sleepHE * 0.9;
            }
            else if (age >= 55 && age < 65)
            {
                valueHE = stepsHE * 0.9 + indoorHE * 1.3 + sleepHE * 0.9;
            }
            else if (age >= 65)
            {
                valueHE = stepsHE * 0.8 + indoorHE * 1.4 + sleepHE * 1.0;
            }
        }

        else if (gender == 1)//女性
        {
            if (age < 18)
            {
                valueHE = stepsHE * 0.65 + indoorHE * 0.85 + sleepHE * 0.8;
            }
            else if (age >= 18 && age < 28)
            {
                valueHE = stepsHE * 0.75 + indoorHE * 0.95 + sleepHE * 0.8;
            }
            else if (age >= 28 && age < 35)
            {
                valueHE = stepsHE * 0.85 + indoorHE * 1.05 + sleepHE * 0.8;
            }
            else if (age >= 35 && age < 45)
            {
                valueHE = stepsHE * 0.95 + indoorHE * 1.15 + sleepHE * 0.9;
            }
            else if (age >= 45 && age < 55)
            {
                valueHE = stepsHE * 1.05 + indoorHE * 1.25 + sleepHE * 0.9;
            }
            else if (age >= 55 && age < 65)
            {
                valueHE = stepsHE * 0.95 + indoorHE * 1.35 + sleepHE * 0.9;
            }
            else if (age >= 65)
            {
                valueHE = stepsHE * 0.85 + indoorHE * 1.45 + sleepHE * 1.0;
            }
        }

        //健康能量等级计算
        if (valueHE < 1800)
        {
            levelHE = 1;
        }
        else if (valueHE >= 1800 && valueHE < 3500)
        {
            levelHE = 2;
        }
        else if (valueHE >= 3500)
        {
            levelHE = 3;
        }

        Debug.Log("Health Energy " + valueHE);
    }
}