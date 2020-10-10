using System;
using System.Linq;
using UnityEngine;
using Mirror;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    // Can drag a scene in the inspector
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    // When a client connect to a server
    public override void OnClientConnect(NetworkConnection conn)
    {
        // Do the base logic
        base.OnClientConnect(conn);

        // Raise my event
        OnClientConnected?.Invoke();
    }

    // When a client disconnects 
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        // Do the base logic
        base.OnClientDisconnect(conn);

        // Raise my event
        OnClientDisconnected?.Invoke();
    }

    // Called on a server, when a client connects
    public override void OnServerConnect(NetworkConnection conn)
    {
        // If we got too many players, 
       if (numPlayers >= maxConnections)
        {
            // Disconnect that person
            conn.Disconnect();
            return;
        }

       // If not in the menu scene
       if (SceneManager.GetActiveScene().path != menuScene)
        {
            // Also disconnect
            conn.Disconnect();
            return;
        }
    }

    // Called on the server when a client adds a new Player
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // If in the menu scene
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public bool IsReadyToStart()
    {
        // If the number of players (people currently connected) is less than the minimum
        if (numPlayers < minPlayers) { return false; }

        // If have enough people
        // We loop over all the people
        foreach (var player in RoomPlayers)
        {
            // If one person is not ready
            if (!player.IsReady) { return false; }
        }

        return true;
    }
   
}
