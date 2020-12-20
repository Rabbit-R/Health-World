using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class AudioData : MonoBehaviour
{
    

    public static float volume;//音量

    private const int VOLUME_DATA_LENGTH = 128;    //录制的声音长度
    private const int frequency = 16000; //码率
    private const int lengthSec = 10;   //录制时长

    private const float maxVolume = 30;//录音开启音量值

    private float interval;

    private  int noise = 0;

    //public static bool noiseFlag;
    public static int noiseCount = 0;

    private int Timer = 0;
    private const bool V = true;

    private AudioSource audioSource;  //录制的音频
    


    public Text statusText;

    
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, lengthSec, frequency);
    }

    private void Update()
    {
        //noiseFlag = false;
        if (interval >= 1 ) // updatea per second
        {
            volume = GetVolume(audioSource.clip, VOLUME_DATA_LENGTH);
            string v = volume.ToString();
            statusText.text = v;
            Timer += 1; // Plus 1 second
            if(volume > maxVolume)
            {
                noise += 1;
            }


            interval = 0;
        }
        
        interval += Time.deltaTime;

        if(Timer == 4) // check result every 6 seconds
        {
            if(noise > 3)
            {
                //noiseFlag = true;
                noiseCount += 1;
            }

            Timer = 0;
            noise = 0;
            //Debug.Log(noiseCount);

        }
    }



    /// <summary>
    /// 获取音量
    /// </summary>
    /// <param name="clip">音频片段</param>
    /// <param name="lengthVolume">长度</param>
    /// <returns></returns>
    private float GetVolume(AudioClip clip, int lengthVolume)
    {
        if (Microphone.IsRecording(null))
        {
            float maxVolume = 0f;
            //用于储存一段时间内的音频信息
            float[] volumeData = new float[lengthVolume];
            //获取录制的音频的开头位置
            int offset = Microphone.GetPosition(null) - (lengthVolume + 1);
            if (offset < 0)
                return 0f;
            //获取数据
            clip.GetData(volumeData, offset);
            //解析数据
            for (int i = 0; i < lengthVolume; i++)
            {
                float tempVolume = volumeData[i];
                if (tempVolume > maxVolume)
                    maxVolume = tempVolume;
            }
            return maxVolume * 99;
        }
        return 0;
    }

   
}


