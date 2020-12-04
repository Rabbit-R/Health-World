using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightController : MonoBehaviour
{
    private string datetimeStr;
    public Material skybox_day;
    public Material skybox_night;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skybox_night;
    }

    // Update is called once per frame
    void Update()
    {
        datetimeStr = System.DateTime.Now.ToString();
        this.GetComponent<Light>().intensity -= 0.001f;
        this.GetComponent<ReflectionProbe>().intensity -= 0.001f;
    }
}
