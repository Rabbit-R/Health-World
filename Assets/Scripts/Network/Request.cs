using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Request : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() => StartCoroutine(GetRequest("http://127.0.0.1:5000/"));

    //IEnumerable can wait until we get the respond from server
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest(); // wait until continue
            if(webRequest.isNetworkError)
            {
                Debug.Log("Error:" + webRequest.error);
            } else {
                Debug.Log(webRequest.downloadHandler.text);
            }
        }

    }
}
