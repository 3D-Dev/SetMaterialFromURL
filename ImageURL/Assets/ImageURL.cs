using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ImageURL : MonoBehaviour
{
    void Start()
    {
        string[] urls = new string[3];
        urls[0] = "https://jpn-exhibition-hall.com/img/Cube01.jpg";
        urls[1] = "https://jpn-exhibition-hall.com/img/Cube02.jpg";
        urls[2] = "https://jpn-exhibition-hall.com/img/Cube03.jpg";
        //https://jpn-exhibition-hall.com/img/Cube01.jpg
        //https://jpn-exhibition-hall.com/img/Cube02.jpg
        //https://jpn-exhibition-hall.com/img/Cube03.jpg
        StartCoroutine(DownloadImage(urls));
    }

    IEnumerator DownloadImage(string[] urls)
    {   
        for(int i = 0; i <urls.Length; i ++) {
            if(urls[i] != "") {
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(urls[i]);
                string filename = Path.GetFileNameWithoutExtension(urls[i]);
                Debug.Log("TextureURL:" + urls[i] + request + filename);
                Texture myTexture = null;
                yield return request.SendWebRequest();
                if(request.isNetworkError || request.isHttpError) 
                    Debug.Log(request.error);
                else
                    myTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;

                if(myTexture) {
                    Debug.Log("URLS:" + filename);
                    GameObject obj = GameObject.Find(filename);
                    if(obj) {
                        obj.GetComponent<Renderer>().material.mainTexture = myTexture;
                    }
                }
            }
        }
        
        
    } 
}
