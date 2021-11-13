using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RoomManager : MonoBehaviour
{
  public enum NetworkType
  {
    Server,
    Client,
    Host
  }
  public NetworkType networkType;
  public bool autoStart;
  // Start is called before the first frame update
  void Start()
  {
    if (autoStart)
    {
      switch (networkType)
      {
        case NetworkType.Server:
          NetworkManager.Singleton.StartServer();
          break;
        case NetworkType.Client:
          NetworkManager.Singleton.StartClient();
          break;
        case NetworkType.Host:
          NetworkManager.Singleton.StartHost();
          break;
      }
    }
    else
    {

    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
