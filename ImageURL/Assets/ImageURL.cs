using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class ImageURL : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject image;
    void Start()
    {
        string[] urls = new string[200];
        GameObject[] objList;
        objList = GameObject.FindGameObjectsWithTag("ImagePanel");
        int i = 0;
        foreach(GameObject obj in objList) {
            urls[i] = "server path" + obj.name + ".jpg";
            i ++;
        }
        if(urls != null)
            StartCoroutine(DownloadImage(urls));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name != "") {
                    StartCoroutine(OnClickObject(hit.collider.gameObject.name));
                }
                
            }
            else {
                return;
            }
        }
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
                else
                    myTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;

                if(myTexture) {
                    GameObject obj = GameObject.Find(filename);
                    if(obj) {
                        obj.GetComponent<Renderer>().material.mainTexture = myTexture;
                    }
                }
            }
        } 
    }


    public void OnCloseButton() {
        Canvas.SetActive(false);
    }

    IEnumerator OnClickObject(string name) {
        string url = "server path" + name + ".jpg";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        Texture2D myTexture = null;
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)             
        else
            myTexture = ((DownloadHandlerTexture) request.downloadHandler).texture as Texture2D;

        if(myTexture) {        
            Sprite mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            image.GetComponent<Image>().overrideSprite = mySprite;
            Canvas.SetActive(true);
        }
    }

}
