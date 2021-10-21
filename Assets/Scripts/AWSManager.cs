using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.S3;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System.IO;
using System;
using UnityEngine.Networking;

public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("AWS_Manager is NULL!");
            return _instance;
        }
    }

    public string S3Region = RegionEndpoint.USEast2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if(_s3Client == null)
            {
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials(
                    "us-east-2:aa404cd0-16c4-4031-8dda-dcdd726a9b7a",
                    RegionEndpoint.USEast2
                    ), _S3Region);
            }
            return _s3Client;
        }
    }

    public GameObject imageTarget;

    private void Awake()
    {
        _instance = this;
        UnityInitializer.AttachToGameObject(gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        var request = new ListObjectsRequest()
        {
            BucketName = "assetsbundlearhorse"
        };

        S3Client.ListObjectsAsync(request, (responseObj) =>
        {
            if(responseObj.Exception == null)
            {
                Debug.Log("Successfully posted to bucket.");
                responseObj.Response.S3Objects.ForEach((obj) =>
                {
                    Debug.Log("obj: " + obj.Key);
                });
            }
            else
            {
                Debug.Log("Exception occured during uploading: " + responseObj.Exception);
            }
        });
        StartCoroutine(BundleRoutine());
        //DownloadBundle();
    }

    //public void DownloadBundle()
    //{
    //    //string bucketName = "assetsbundlearhorse";
    //    //string fileName = "horse";

    //    //S3Client.GetObjectAsync(bucketName, fileName, (responseObj) =>
    //    //{
    //    //    if(responseObj.Exception == null)
    //    //    {
    //    //        string data = null;
    //    //        using(StreamReader reader = new StreamReader(responseObj.Response.ResponseStream))
    //    //        {
    //    //            data = reader.ReadToEnd();
    //    //            Debug.Log("Data: " + data);
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        Debug.Log("Exception occured: " + responseObj.Exception);
    //    //    }
    //    //});
    //    StartCoroutine(BundleRoutine());
    //}

    IEnumerator BundleRoutine()
    {
        string uri = "https://assetsbundlearhorse.s3.us-east-2.amazonaws.com/horse";
        var request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.SendWebRequest();

        Debug.Log("Get assets: " + request);

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        Debug.Log("Get assets: " + bundle.ToString());
        GameObject horse = bundle.LoadAsset<GameObject>("horse");
        horse = Instantiate(horse);

        horse.transform.parent = imageTarget.transform;
        horse.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
