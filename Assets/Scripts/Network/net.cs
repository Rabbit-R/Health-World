using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class net : MonoBehaviour
{
    
    public Post request; //Request

    //#if UNITY_EDITOR
    string FilePath = Application.streamingAssetsPath + "/audio.wav";
    //#elif UNITY_ANDROID && !UNITY_EDITOR
    //string FilePath = Application.persistentDataPath + "/audio.wav";
    
    //#endif

    string url = "http://192.168.31.201:5000/upload";
    //string url = "http://127.0.0.1:5000/upload";
    

    public float volume;//音量

    private const int VOLUME_DATA_LENGTH = 128;    //录制的声音长度
    private const int frequency = 16000; //码率
    private const int lengthSec = 10;   //录制时长
    private const float minVolume = 3;//录音关闭音量值
    private const float maxVolume = 15;//录音开启音量值
    private const int minVolume_Sum = 15;//小音量总和值

    private AudioSource audioSource;  //录制的音频
    private bool isRecord;//录音开关
    private bool isStart;//录音开启的起点
    private int minVolume_Number;//记录的小音量数量
    private int start;//录音起点
    private int end;//录音终点

    private float startTime = 0.0f;
    private float endTime = 0.0f;

    private bool updatedFlag = false;

    private float a = 0.0f;
    private float b = 0.0f;

    private float interval;

    public Text statusText;
    public Text statusSave;
 
    
    private void Start()
    {
        

        //File.Copy(Application.streamingAssetsPath + "/audio.wav", Application.persistentDataPath  + "/audio.wav", true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, lengthSec, frequency);
        //Post.flag = true;
       
    }

    private void Update()
    {
        
        volume = GetVolume(audioSource.clip, VOLUME_DATA_LENGTH);
        RecordOpenClose();

        //post audio to the server
        a = Time.fixedTime;
        interval = a - b;
        if(interval > 5 )
        {
             StartCoroutine(IEUpload(url, FilePath));
             b =  Time.fixedTime;
             updatedFlag = false;
        }
        statusSave.text = volume.ToString();
       
        
    }

    public IEnumerator IEUpload(string url, string filePath)
    {
        //flag = false;
        //Debug.Log("1");
        
        UnityWebRequest file = new UnityWebRequest();
        WWWForm form = new WWWForm();
        file = UnityWebRequest.Get(filePath);
        yield return file.SendWebRequest();
 
        form.AddBinaryData("files",file.downloadHandler.data, Path.GetFileName(filePath));
        
        UnityWebRequest req = UnityWebRequest.Post(url, form);
        //UnityWebRequest req = UnityWebRequest.Post(ConfigData.Instance.serverURL + HttpController.UploadFilePath,form);
        yield return req.SendWebRequest();
 
        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            Debug.Log("Upload Success");
            statusSave.text = "Uploaded";
            Debug.Log(req.downloadHandler.text);
            statusText.text = req.downloadHandler.text;
            
            //flag = true;
        }
    }


    /// <summary>
    /// 录音自动开关
    /// </summary>
    private void RecordOpenClose()
    {
        //开
        if (GetVolume(audioSource.clip, VOLUME_DATA_LENGTH) >= maxVolume)
        {
            if (!isStart)
            {
                isStart = true;
                start = Microphone.GetPosition(Microphone.devices[0]);
                startTime = Time.fixedTime; // record start time
            }
            minVolume_Number = 0;
            isRecord = true;
        }
        //关
        if (isRecord && GetVolume(audioSource.clip, VOLUME_DATA_LENGTH) < minVolume)
        {
            endTime = Time.fixedTime; // recording time now
            if (minVolume_Number > minVolume_Sum && endTime - startTime >= 2)
            {
                end = Microphone.GetPosition(Microphone.devices[0]);
                minVolume_Number = 0;
                isRecord = false;
                isStart = false;

                //WavFromClip(Application.streamingAssetsPath + "/test.wav", audioSource.clip);

                byte[] playerClipByte = AudioClipToByte(audioSource.clip, start, end);
                // convert byte to .wav format
                //pc:
                File.WriteAllBytes(Application.streamingAssetsPath + "/audio.wav", playerClipByte); 
                //android:
                //File.WriteAllBytes(Application.persistentDataPath  + "/audio.wav", playerClipByte); 
                
                Debug.Log("save");
                statusSave.text = "save";

                FileStream Stream = new FileStream(Application.streamingAssetsPath + "/audio.wav", FileMode.OpenOrCreate);
                //android: 
                //FileStream Stream = new FileStream(Application.persistentDataPath  + "/audio.wav", FileMode.OpenOrCreate);
                WriteHeader(Stream, audioSource.clip);

                updatedFlag = true;

               

            
                

                
            }
            minVolume_Number++;

           
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

    /// <summary>
    /// clip转byte[]
    /// </summary>
    /// <param name="clip">音频片段</param>
    /// <param name="star">开始点</param>
    /// <param name="end">结束点</param>
    /// <returns></returns>
    public byte[] AudioClipToByte(AudioClip clip, int star, int end)
    {
        float[] data;
        if (end > star)
            data = new float[end - star];
        else
            data = new float[clip.samples - star + end];
        clip.GetData(data, star);
        int rescaleFactor = 32767; //to convert float to Int16
        byte[] outData = new byte[data.Length * 2];
        for (int i = 0; i < data.Length; i++)
        {
            short temshort = (short)(data[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }
        return outData;
    }

    
    

    //// <summary>
    /// Add wav. format Header to byte
    /// </summary>

    private void WriteHeader(FileStream stream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        //BinaryWriter bw = new BinaryWriter(stream);

        stream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        stream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
        stream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        stream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        stream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        stream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        stream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        stream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        stream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2  
        stream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        stream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        stream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        stream.Write(subChunk2, 0, 4);

    }

}


