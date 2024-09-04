// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using FishNet.Managing;

// public class ServerClient : MonoBehaviour
// {
//     // Start is called before the first frame update


//     public string serverIPAddress = "127.0.0.1";
//     public ushort serverPort = 7777;


//     void Start()
//     {
//         NetworkManager networkManager = GetComponent<NetworkManager>();

//         networkManager.TransportManager.Transport.SetServerBindAddress(serverIPAddress);
//         networkManager.TransportManager.Transport.SetPort(serverPort);

//         networkManager.ServerManager.StartServer();
//     }


//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
