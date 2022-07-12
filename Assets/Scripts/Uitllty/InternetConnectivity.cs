using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class InternetConnectivity : MonoBehaviour
{
    public static InternetConnectivity Instance { get; private set; }

    [SerializeField] private Canvas internetLoadingCanvas;

    public UnityAction OnDisconnect;
    public UnityAction OnConnected;

    private float nextCheckTime;
    public bool isConnected;
    

    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        DontDestroyOnLoad(this);
      
    }
    void Update()
    {
        if(nextCheckTime <= Time.time)
        {
            nextCheckTime = Time.time + 1;
            StopAllCoroutines();
            StartCoroutine(checkInternetConnection());

            //if(Application.internetReachability != NetworkReachability.NotReachable)
            //{
            //    Connect();
            //}
            //else
            //{
            //    Disconnect();
            //}
            //if (PhotonChatClient.chatClient != null)
            //{
            //    if (!PhotonChatClient.chatClient.CanChat)
            //    {
            //        Disconnect();
            //    }
            //    else
            //    {
            //        Connect();

            //    }
            //}
        }
    }
    IEnumerator checkInternetConnection()
    {
        const string echoServer = "http://google.com";

        bool result;
        using (var request = UnityWebRequest.Head(echoServer))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();
            result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
        }
        if (!result)
        {
            Disconnect();
        }
        else
        {
            Connect();
        }
        isConnected = result;
    }

    private void Connect()
    {
        OnDisconnect?.Invoke();
        InternetLoadingCanvas(false);
    }
    private void Disconnect()
    {
        OnConnected?.Invoke();
        InternetLoadingCanvas(true);
    }
    private void InternetLoadingCanvas(bool state)
    {
        if(internetLoadingCanvas == null)
        {
            GameObject obj = Instantiate(Resources.Load("InternetLoadingCanvas"), Vector3.zero, Quaternion.identity) as GameObject;
            internetLoadingCanvas = obj.GetComponent<Canvas>();
        }
        internetLoadingCanvas.enabled = state;
    }
}
