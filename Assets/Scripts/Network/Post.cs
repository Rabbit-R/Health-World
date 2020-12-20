using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class Post : MonoBehaviour
{   
    public static bool flag;
    public static Text statusText;
    
    string FilePath = "C:/Tony/Unity/Health-World/Assets/StreamingAssets/audio.wav";
    //C:/Tony/Unity/Health-World/Assets/StreamingAssets/audio.wav
    //"C:/Tony/Code/Audio Classification/Script/keyboard.wav"
    string url = "http://192.168.31.201:5000/upload";
    //192.168.31.201：5000/upload
    //"http://127.0.0.1:5000/upload";
   
   /*
    private void  Upload()
    {
        StartCoroutine(IEUpload(url, FilePath));
    }
   

   private void Update()
   {
       StartCoroutine(IEUpload(url, FilePath));
   }
   */
    void Start() => StartCoroutine(IEUpload(url, FilePath));

    public IEnumerator IEUpload(string url, string filePath)
    {
        //flag = false;
        Debug.Log("1");
        
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
            Debug.Log(req.downloadHandler.text);
            statusText.text = req.downloadHandler.text;
            
            //flag = true;
        }
    }
 
 
    private void CallBack(string str)
    {
        Debug.Log("Upload Complete : " +  str);
    }
}